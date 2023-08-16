using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Wrappers
{
    internal class PlayerUndirectSkill<T> : PlayerActiveSkill<T> where T : Player
    {
        public override bool TryActivate(SkillHolder holder, Player player, bool clickedByIcon)
        {
            Editor.Editor.Instance.PreviewManager.PerformAction(new LevelEditor.PreviewManager.Config((Vector2 v) => OnActivate(player as T), holder) { Status = LevelEditor.PreviewManager.PreviewStatus.DISABLED});
            var playerSpace = Editor.Editor.Instance.PlayerSpace;

            if (TimeSinceActivation < BaseCooldown || !playerSpace.TrySpendMana(BaseManacost)) return false;

            ResetTimer();
            return true;
        }
        protected virtual void OnActivate(T player)
        {

        }
    }
}
