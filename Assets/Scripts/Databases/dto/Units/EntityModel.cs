using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Databases.dto.Units
{
    internal abstract class EntityModel : ScriptableObject
    {
        [SerializeField] protected Entity _entity;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ObjectDescription _description;
        public virtual void Configure() { }
        public abstract Model GetModel();

        public class Model
        {
            public Sprite Icon;
            public Entity Entity;
            public ObjectDescription Description { get; private set; }
            public IPlaceable Placeable;

            public Model(EntityModel model) 
            {
                Entity = model._entity;
                Placeable = model._entity as IPlaceable;
                Description = model._description;
                Icon = model._icon;
            }
        }
    }
}
