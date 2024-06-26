using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Config;
using Assets.Scripts.Databases.dto;
using Assets.Scripts.Databases.dto.Units;
using Assets.Scripts.Databases.LevelDatabases;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Skills;
using Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.Interfaces;
using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Entities.Util.Config.Input;
using Assets.Scripts.Entities.Util.Cooldown;
using Assets.Scripts.Level;
using Assets.Scripts.LevelEditor;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

internal class RuntimeSystem : PlacementSystem
{
    [SerializeField] private RuntimeDatabase _defaultRuntimeDatabase;
    private RuntimeDatabase _currentRuntimeDatabase;
    private List<SkillHolder> skills = new();
    public List<RuntimeEntityModel> _entitiesDatabaseInstance = new();
    private List<RuntimeEntityHolder> runtimeEntities = new();
    private RuntimeEntityHolder entityHolder;
    private Player _player;
    public bool DisablePlacingEntities;
    public Action OnEntityPlaced;
    public bool CastThroughGameObjects { get; set; }
    protected override void Awake()
    {
        _currentRuntimeDatabase = _defaultRuntimeDatabase;
        ConfigureDatabase();
		LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnLevelModeChanged;
		entityHolder = Resources.Load<RuntimeEntityHolder>("UI/RuntimeEntityHolder");
        base.Awake();
    }
    private void ConfigureDatabase()
    {
        foreach (var e in _currentRuntimeDatabase.Entities)
        {
            e.Configure();
        }
    }
    public void SetRuntimeDatabase(RuntimeDatabase database)
    {
        if (database == null) _currentRuntimeDatabase = _defaultRuntimeDatabase;
        else _currentRuntimeDatabase = database;
        ConfigureDatabase();
    }
    protected override bool AllowUsingEntityID(int id)
    {
        var model = _entitiesDatabaseInstance[id].GetRuntimeModel();
        return CooldownStrategy.IsCooldownPassed(model) == true && Editor.Instance.PlayerSpace.IsEnoughMana(model.BaseManacost);
    }
    private void Update()
    {
        foreach(var skill in Editor.Instance.PlayerSpace.GetPlayerSkillModels())
        {
            skill.Skill.Update();
        }
        if(OnPlace != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Editor.Instance.GameModeIs(GameMode.RUNTIME) && (EventSystem.current.IsPointerOverGameObject() == false || CastThroughGameObjects) && selectedEntityIndex >= 0)
            {
                Editor.Instance.PreviewManager.PerformAction(new PreviewManager.Config(OnPlace, selectedHolder) { Status = PreviewManager.PreviewStatus.DISABLED });
            }
        }
        if (Editor.Instance.GameModeIs(GameMode.RUNTIME) && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Editor.Instance.PreviewManager.Deselect();
        }
        if (_entitiesDatabaseInstance != null && runtimeEntities != null) UpdateRuntimeEntities();
    }
    private void UpdateRuntimeEntities()
    {
        for(int i = 0; i < _entitiesDatabaseInstance.Count; i++)
        {
            var entity = _entitiesDatabaseInstance[i];
            var model = entity.GetRuntimeModel();
            if (CooldownStrategy.IsCooldownPassed(model, false) == false)
            {
                model.TimeSinceActivation += Time.deltaTime;
                runtimeEntities[i].UpdateCooldownValue();
            }
        }
    }
    public override void FillContainer()
    {
        SkillHolder skillHolder = Resources.Load<SkillHolder>("UI/SkillHolder");
        for (int i = 0; i < _currentRuntimeDatabase.Entities.Count; i++)
        {
            RuntimeEntityHolder holder = Instantiate(entityHolder, editor.RuntimeHolderContainer);

            var instanceModel = Instantiate(_currentRuntimeDatabase.Entities[i]);
            var model = _currentRuntimeDatabase.Entities[i].GetRuntimeModel();
            instanceModel.Configure();

            holder.Init(i, _currentRuntimeDatabase, this, GetRuntimeAbilityKey(i));
            holder.Configure(model.BaseManacost, instanceModel.GetRuntimeModel());

            _entitiesDatabaseInstance.Add(instanceModel);
            runtimeEntities.Add(holder);
        }

        List <PlayerSkillModel.Model> list = Editor.Instance.PlayerSpace.GetPlayerSkillModels();
        if (_player == null) _player = FindObjectOfType<Player>();

        for (int i = 0; i < list.Count; i++)
        {
            var obj = Instantiate(skillHolder, editor.RuntimePlayerContainer);

            obj.Init(list[i].Skill, _player, GetHeroAbilityKey(i));
            skills.Add(obj);
        }
    }
    private void ResetSkillsCooldown()
    {
        foreach(var skill in Editor.Instance.PlayerSpace.GetPlayerSkillModels())
        {
            var cooldownable = skill.Skill as ICooldownResetter;
            cooldownable.ResetCooldown();
        }
    }
    private KeyCode GetHeroAbilityKey(int i)
    {
        switch(i)
        {
            case 0: return ConfigManager.Instance.InputConfig.Keys[InputConfig.ActionKey.FIRST_HERO_ABILITY];
            case 1: return ConfigManager.Instance.InputConfig.Keys[InputConfig.ActionKey.SECOND_HERO_ABILITY];
            case 2: return ConfigManager.Instance.InputConfig.Keys[InputConfig.ActionKey.THIRD_HERO_ABILITY];
        }
        return KeyCode.None;
    }
    private KeyCode GetRuntimeAbilityKey(int i)
    {
        switch (i)
        {
            case 0: return ConfigManager.Instance.InputConfig.Keys[InputConfig.ActionKey.FIRST_RUNTIME_ABILITY];
            case 1: return ConfigManager.Instance.InputConfig.Keys[InputConfig.ActionKey.SECOND_RUNTIME_ABILITY];
            case 2: return ConfigManager.Instance.InputConfig.Keys[InputConfig.ActionKey.THIRD_RUNTIME_ABILITY];
            case 3: return ConfigManager.Instance.InputConfig.Keys[InputConfig.ActionKey.FOURTH_RUNTIME_ABILITY];
            case 4: return ConfigManager.Instance.InputConfig.Keys[InputConfig.ActionKey.FIVETH_RUNTIME_ABILITY];
            case 5: return ConfigManager.Instance.InputConfig.Keys[InputConfig.ActionKey.SIXTH_RUNTIME_ABILITY];
        }
        return KeyCode.None;
    }
    public override void ClearContainer()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            Destroy(skills[i].gameObject);
        }
        skills.Clear();

        for (int i = 0; i < runtimeEntities.Count; i++)
        {
            Destroy(runtimeEntities[i].gameObject);
        }
        runtimeEntities.Clear();
        for(int i = 0; i < _entitiesDatabaseInstance.Count; i++)
        {
            Destroy(_entitiesDatabaseInstance[i]);
        }
        _entitiesDatabaseInstance.Clear();
    }
    private void PlaceEntity(Vector2 cursorPos)
    {
        if ((editor._inputManager.IsPointerOverUI() && CastThroughGameObjects == false) || DisablePlacingEntities) return;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(cursorPos);
        bool validity = CheckPlacementValidity(gridPos, selectedEntityIndex);
        if (validity == false) return;
        var model = _entitiesDatabaseInstance[selectedEntityIndex].GetRuntimeModel();
        Editor.Instance.PlayerSpace.SpendMana(model.BaseManacost);
        model.TimeSinceActivation = 0;

        if (validity == true)
        {
            var entity = Instantiate(model.Entity, grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y)), Quaternion.identity);
        }
        OnEntityPlaced?.Invoke();
        selectedEntityIndex = -1;
    }
    public bool CheckPlacementValidity(Vector2Int gridPosition, int selectedEntityIndex)
    {
        if(selectedEntityIndex < 0) return false;
        if (editor.LimitingTileMap.HasTile(new Vector3Int(gridPosition.x, gridPosition.y)) ||
            !editor.PlacementTileMap.HasTile(new Vector3Int(gridPosition.x, gridPosition.y)))
        {
            return false;
        }
        return true;
    }
    public void SignMethods(bool active)
    {
        if (active == true)
        {
			OnPlace += PlaceEntity;
        }
        else
        {
            OnPlace -= PlaceEntity;
        }
    }
	private void OnDisable()
    {
		OnPlace -= PlaceEntity;
    }

	public void OnLevelModeChanged(GameMode mode)
	{
        if (mode == GameMode.RUNTIME) ResetSkillsCooldown();
	}
}
