using UnityEngine;
using Assets.Scripts.Databases.dto.Runtime;
using Assets.Scripts.Entities.Placeable;

internal class EditorEntityHolder : ObjectHolder
{
    public void Configure(IEditorSpaceRequired spaceRequired)
    {
        Cost.text = spaceRequired.SpaceRequired.ToString();
    }
}
