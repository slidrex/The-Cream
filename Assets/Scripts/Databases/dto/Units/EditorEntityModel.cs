using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.Entities;
using System;
using UnityEngine;
using Assets.Scripts.Databases.dto.Units;

namespace Assets.Scripts.Databases.dto.Runtime
{
    [CreateAssetMenu(menuName = "Cream/Database/DTO/Editor Entity")]
    [Serializable]
    internal class EditorEntityModel : EntityModel
    {
        public byte Size;
        private EntityModel.Model model;
        public override EntityModel.Model GetModel() => model;
        public override void Configure()
        {
            model = new EditorModel(Size, _entity, this);
        }

        public class EditorModel : Model
        {
            public byte Size;
            public IEditorSpaceRequired EditorSpaceRequired;

            public EditorModel(byte size, BaseEntity entity, EntityModel model) : base(model)
            {
                Size = size;
                EditorSpaceRequired = entity as IEditorSpaceRequired;
            }
        }
    }
}
