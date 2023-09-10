using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Projectiles;

namespace Entities.Player.Skills.Implementations.LightEater
{
    internal class EaterAttackProjectile : AttackProjectile<Assets.Scripts.Entities.Player.Characters.LightEater>
    {
        public override EntityTypeBase TriggerEntityType => new EntityType<MobTag>().Any();
        protected override void OnTargetHit(Entity hit)
        {
            base.OnTargetHit(hit);
            Destroy(gameObject);
        }
    }
}