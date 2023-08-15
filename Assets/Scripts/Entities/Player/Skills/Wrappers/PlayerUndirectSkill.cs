using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Player.Skills.Wrappers
{
    internal class PlayerUndirectSkill<T> : PlayerActiveSkill<T> where T : Player
    {
        public override bool TryActivate(SkillHolder holder, Player player)
        {
            Editor.Editor.Instance._runtimeSystem.Deselect();
            var playerSpace = Editor.Editor.Instance.PlayerSpace;

            if (TimeSinceActivation < BaseCooldown || !playerSpace.TrySpendMana(BaseManacost)) return false;
            OnActivate(player as T);

            ResetTimer();
            return true;
        }
        protected virtual void OnActivate(T player)
        {

        }
    }
}
