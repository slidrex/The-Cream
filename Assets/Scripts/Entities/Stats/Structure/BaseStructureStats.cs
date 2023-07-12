using Assets.Scripts.Entities.Stats.Interfaces.Stats;

namespace Assets.Scripts.Entities.Stats.Structure
{
    internal class BaseStructureStats : EntityStats, IHaveTargetRadius
    {
        public float TargetRadius { get; set; }
        public BaseStructureStats(float targetRadius)
        {
            TargetRadius = targetRadius;
        }
    }
}
