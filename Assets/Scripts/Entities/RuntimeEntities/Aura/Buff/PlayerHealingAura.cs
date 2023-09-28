using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using UnityEngine;

namespace Assets.Scripts.Entities.RuntimeEntities.Aura.Buff
{
    internal class PlayerHealingAura : DurationAura
    {
        [UnityEngine.SerializeField] private float _percentHealing;
        [UnityEngine.SerializeField] private float _healingInterval;
        protected override EntityTypeBase TargetType => new EntityType<PlayerTag>(PlayerTag.PLAYER);

        protected override void OnBlockTimeExpired(Entity entity)
        {
            entity.SpriteRenderer.color = entity.DefaultColor;
        }
        protected override void UpdateTargetEntityInside(Entity entity, out float blockTime)
        {
            var heal = new InstantHeal(entity, _percentHealing);
            entity.Stats.ModifierHolder.AddModifier(heal);
            entity.SpriteRenderer.color = Color.red;
            blockTime = _healingInterval;
        }
    }
}
