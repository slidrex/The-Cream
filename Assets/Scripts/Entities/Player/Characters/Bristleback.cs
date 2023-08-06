using Assets.Scripts.Entities.Stats.StatAttributes;

namespace Assets.Scripts.Entities.Player.Characters
{
    internal class Bristleback : Player
    {
        public override void OnLevelUp()
        {
            Stats.Modify<MaxHealthStat>(new Entities.Stats.StatAttributes.AttributeMask() { BaseValue = 10 });
            print("I have a good news!");
        }
    }
}
