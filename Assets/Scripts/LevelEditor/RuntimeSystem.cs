using Assets.Editor;
using Assets.Scripts.Databases.dto.Units;
using Assets.Scripts.Entities.Player;
using System.Collections.Generic;
using UnityEngine;

internal class RuntimeSystem : PlacementSystem
{
    private List<SkillHolder> skills = new();
    private List<RuntimeEntityHolder> runtimeEntities = new();
    private RuntimeEntityHolder entityHolder;
    private Player _player;
    
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && editor.GameModeIs(Editor.GameMode.RUNTIME))
        {
            OnPlace?.Invoke();
        }
    }
    public override void FillContainer()
    {
        SkillHolder skillHolder = Resources.Load<SkillHolder>("UI/SkillHolder");
        for (int i = 0; i < database.Entities.Count; i++)
        {
            RuntimeEntityHolder obj = Instantiate(entityHolder, editor.RuntimeHolderContainer);
            obj.Init(i, database, this);
            runtimeEntities.Add(obj);
        }

        List <PlayerSkillModel.Model> list = Editor.Instance.PlayerSpace.GetPlayerSkillModels();
        if (_player == null) _player = FindObjectOfType<Player>();

        for (int i = 0; i < list.Count; i++)
        {
            var obj = Instantiate(skillHolder, editor.RuntimePlayerContainer);
            obj.Init(list[i].Skill, _player);
            skills.Add(obj);
        }
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
