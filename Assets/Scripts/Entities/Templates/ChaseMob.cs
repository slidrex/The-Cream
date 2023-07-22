﻿using Assets.Scripts.Entities.Attack;
using Assets.Scripts.Entities.Movement;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.Templates;
using Assets.Scripts.Entities.Stats.Strategies;

namespace Assets.Scripts.Entities.Templates
{
    [UnityEngine.RequireComponent(typeof(MobBaseAttack))]
    [UnityEngine.RequireComponent(typeof(EntityMovement))]
    internal abstract class ChaseMob : Entity, IBaseMobStatsProvider
    {
        public override EntityTypeBase ThisType => new EntityType<MobTag>(MobTag.AGGRESSIVE);
        public int CurrentHealth { get; set; }

        public abstract byte SpaceRequired { get; }

        public void Damage(int damage, Entity deler)
        {
            EntityHealthStrategy.Damage(this, damage, deler);
        }

        public void Heal(int heal)
        {
            EntityHealthStrategy.Heal(this, heal);
        }
        
        public void OnContruct()
        {
            
            Editor.Editor.Instance._spaceController.ChangeSpace(SpaceRequired);
        }

        public void OnDeconstruct()
        {
            Editor.Editor.Instance._spaceController.ChangeSpace(-SpaceRequired);
        }

        public virtual void OnDie()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            EntityHealthStrategy.ResetHealth(this);
        }
    }
}
