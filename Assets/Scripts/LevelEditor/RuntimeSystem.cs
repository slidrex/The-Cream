using Assets.Editor;
using Assets.Scripts.Databases.dto.Units;
using Assets.Scripts.Entities.Player;
using System.Collections.Generic;
using UnityEngine;

internal class RuntimeSystem : PlacementSystem
{
    protected override void Start()
    {
        base.Start();
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
    public void FillContainer()
    {
        SkillHolder skillHolder = Resources.Load<SkillHolder>("UI/SkillHolder");
        for (int i = 0; i < database.Entities.Count; i++)
        {
            EntityHolder obj = Instantiate(entityHolder, editor.RuntimeHolderContainer);
            obj.Init(i, database, this);
        }

        List <PlayerSkillModel.Model> list = Editor.Instance.PlayerSpace.GetPlayerSkillModels();
        Player player = Editor.Instance.PlayerSpace.GetCharacterModel().Player;
        for (int i = 0; i < list.Count; i++)
        {
            var obj = Instantiate(skillHolder, editor.RuntimePlayerContainer);
            obj.Init(list[i].Skill, player);
        }
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
