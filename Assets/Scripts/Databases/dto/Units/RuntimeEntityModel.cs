using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Databases.dto
{
    [CreateAssetMenu(menuName = "Cream/Database/DTO/Runtime Entity")]
    internal class RuntimeEntityModel : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private BaseEntity _entity;
        [SerializeField] private int _baseManacost;
        [SerializeField] private float _cooldown;
        public Model GetModel() => new(this);
        public struct Model
        {
            public Sprite Icon;
            public BaseEntity Entity;
            public int BaseManacost;
            public float Cooldown;
            public Model(RuntimeEntityModel model)
            {
                Icon = model._icon;
                Entity = model._entity;
                BaseManacost = model._baseManacost;
                Cooldown = model._cooldown;
            }
        }
    }
}
