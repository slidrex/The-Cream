﻿using Assets.Scripts.Entities.Util.Cooldown;
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
            if (player.IsMuted) return false;
            var playerSpace = Editor.Editor.Instance.PlayerSpace;

            if (!CooldownStrategy.IsCooldownPassed(this) || DisableActivate || !playerSpace.TrySpendMana(BaseManacost)) return false;
            Editor.Editor.Instance.PreviewManager.PerformAction(new LevelEditor.PreviewManager.Config((Vector2 v) => OnActivate(player as T), holder) { Status = LevelEditor.PreviewManager.PreviewStatus.DISABLED});

            ResetCooldown();
            return true;
        }
        protected virtual void OnActivate(T player)
        {

        }
    }
}
