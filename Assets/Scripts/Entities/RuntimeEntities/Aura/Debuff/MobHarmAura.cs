using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Entities.RuntimeEntities.Aura.Debuff
{
    internal class MobHarmAura : Aura
    {
        [SerializeField] private int _damage;
        [SerializeField] private GameObject _particles;
        protected override EntityTypeBase TargetType => new EntityType<MobTag>().Any();
        private void Start()
        {
            var entities =  NavigationUtil.GetAllEntitiesOfType(TargetType, transform, Radius);
            foreach (var entity in entities)
            {
                entity.Stats.ModifierHolder.AddModifier(new InstantDamage(entity, _damage));
            }
            ParticlesUtil.SpawnParticles(_particles, transform);
            Destroy(gameObject);
        }
    }
}
