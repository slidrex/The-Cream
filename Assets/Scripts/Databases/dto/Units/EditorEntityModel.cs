using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.Entities;
using System;
using UnityEngine;

namespace Assets.Scripts.Databases.dto.Runtime
{
    [CreateAssetMenu(menuName = "Cream/Database/DTO/Editor Entity")]
    internal class EditorEntityModel : ScriptableObject
    {
        public Sprite Icon;
        public byte Size;
        public BaseEntity Entity;
        
        public Model GetModel()
        {
            IPlaceable place = Entity as IPlaceable;
            IEditorSpaceRequired req = Entity as IEditorSpaceRequired;
            if (place == null || req == null) throw new NullReferenceException();
            return new Model(Entity, Size, place, req, Icon);
        }
        public struct Model
        {
            public Sprite Icon;
            public byte Size;
            public BaseEntity Entity;
            public IPlaceable Placeable;
            public IEditorSpaceRequired EditorSpaceRequired;

            public Model(BaseEntity entity, byte size, IPlaceable placeable, IEditorSpaceRequired editorSpaceRequired, Sprite icon) : this()
            {
                Entity = entity;
                Size = size;
                Placeable = placeable;
                EditorSpaceRequired = editorSpaceRequired;
                Icon = icon;
            }
        }
    }
}
