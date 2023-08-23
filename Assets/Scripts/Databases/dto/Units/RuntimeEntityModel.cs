using Assets.Scripts.Databases.dto.Units;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Databases.dto.Runtime.EditorEntityModel;

namespace Assets.Scripts.Databases.dto
{
    [CreateAssetMenu(menuName = "Cream/Database/DTO/Runtime Entity")]
    [Serializable]
    internal class RuntimeEntityModel : EntityModel
    {
        [SerializeField] private int _baseManacost;
        [SerializeField] private float _cooldown;
        private RuntimeModel model;
        public override Model GetModel() => model;
        public RuntimeModel GetRuntimeModel() => model;
        public override void Configure()
        {
            model = new RuntimeModel(_baseManacost, _cooldown, this);
        }
        public class RuntimeModel : Model, ICooldownable
        {
            public int BaseManacost;
            public float BaseCooldown { get; set; }
            public float TimeSinceActivation { get; set; }

            public RuntimeModel(int manaCost, float cooldown, EntityModel model) : base(model)
            {
                BaseCooldown = cooldown;
                BaseManacost = manaCost;
            }
        }
    }
}
