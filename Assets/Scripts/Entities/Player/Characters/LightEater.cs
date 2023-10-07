using Assets.Scripts.Entities.Player.Skills.Implementations.LightEater;
using Assets.Scripts.Entities.Stats.Interfaces.Attack;
using Assets.Scripts.Entities.Stats.Interfaces.Detect;
using Assets.Scripts.Entities.Stats.StatAttributes.Stats;
using Assets.Scripts.Entities.Stats.StatAttributes;

namespace Assets.Scripts.Entities.Player.Characters
{
    [UnityEngine.RequireComponent(typeof(ShadowWalk))]
    internal class LightEater : Player, IUndetectable, IAttackMutable
    {
        public bool IsUndetectable { get; set; }
        public bool MutedAttack { get; set; }
        public override AttributeHolder Stats { get; } = new AttributeHolder(new MaxHealthStat(36), new SpeedStat(4.5f), new DamageStat(8), new AttackSpeedStat(3));
        public override void OnLevelUp()
        {
            Stats.Modify<SpeedStat>(new Entities.Stats.StatAttributes.AttributeMask() { MaskMultiplier = 0.05f });
        }
    }
}
