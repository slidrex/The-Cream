using Assets.Editor;
using Assets.Scripts.Config;
using Assets.Scripts.Databases.dto.Units;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Util.Config.Input;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

internal class RuntimeSystem : PlacementSystem
{
    private List<SkillHolder> skills = new();
    private List<RuntimeEntityHolder> runtimeEntities = new();
    private RuntimeEntityHolder entityHolder;
    private Player _player;
    public Action OnSelect;
    private ObjectHolder _selectedHolder;
    
    protected override void Awake()
    {
        entityHolder = Resources.Load<RuntimeEntityHolder>("UI/RuntimeEntityHolder");
        base.Awake();
    }
    private void Update()
    {
        foreach(var skill in Editor.Instance.PlayerSpace.GetPlayerSkillModels())
        {
            skill.Skill.Update();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && Editor.Instance.GameModeIs(GameMode.RUNTIME) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            OnPlace?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Deselect();
        }
    }
    public override void FillContainer()
    {
        SkillHolder skillHolder = Resources.Load<SkillHolder>("UI/SkillHolder");
        for (int i = 0; i < database.Entities.Count; i++)
        {
            RuntimeEntityHolder obj = Instantiate(entityHolder, editor.RuntimeHolderContainer);
            obj.Init(i, database, this, GetRuntimeAbilityKey(i));
            runtimeEntities.Add(obj);
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
    }
    private void PlaceEntity()
    {
        if (editor._inputManager.IsPointerOverUI()) return;
        Vector2Int gridPos = (Vector2Int)grid.WorldToCell(editor._inputManager.GetCursorPosition());
        bool validity = CheckPlacementValidity(gridPos, selectedEntityIndex);
        if (validity == false) return;
        var model = database.Entities[selectedEntityIndex].GetModel();

        if (validity == true)
        {
            var entity = Instantiate(model.Entity, grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y)) + new Vector3(model.Size, model.Size) / 2, Quaternion.identity);
        }
        Deselect();
    }
    public void SelectHolder(ObjectHolder holder)
    {
        if (_selectedHolder != null) _selectedHolder.SetActiveSelectImage(false);
        if(holder != null) holder.SetActiveSelectImage(true);

        Editor.Instance._inputManager.SetActivePreviewEntity(holder != null);
        OnSelect.Invoke();
        _selectedHolder = holder;
    }
    public void Deselect()
    {
        selectedEntityIndex = -1;
        OnSelect.Invoke();
        SelectHolder(null);
        Editor.Instance._inputManager.SetActivePreviewEntity(false);
    }
    protected override void OnAfterSetCurrentEntityId()
    {
        Editor.Instance._inputManager.SetActivePreviewEntity(true);
        OnSelect?.Invoke();
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
}
