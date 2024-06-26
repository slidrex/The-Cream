﻿using Assets.Editor;
using Assets.Scripts.Entities.Util.Cooldown;
using Assets.Scripts.LevelEditor.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Wrappers
{
    internal class PlayerDirectSkill<T> : PlayerActiveSkill<T> where T : Player
    {
        private const float INFINITE = -1;
        private Action OnAbilityActivate;
        protected virtual float MaxCastDistance { get; } = INFINITE; 
        public override bool TryActivate(SkillHolder holder, Player player, bool clickedByIcon)
        {
            if (player.IsMuted) return false;
            var playerSpace = Editor.Editor.Instance.PlayerSpace;

            if (CooldownStrategy.IsCooldownPassed(this) == false || !playerSpace.IsEnoughMana(BaseManacost) || DisableActivate) return false;
            
            OnStartSelecting(holder, player as T, clickedByIcon);
            
            return true;
        }
        private void OnStartSelecting(SkillHolder holder, T player, bool clickedByIcon)
        {
            AbilityAdapter.Instance.StartAbilityPreview((Vector2 castPos) => OnAbilActivate(castPos, player), holder, clickedByIcon, MaxCastDistance == INFINITE ? null : new LevelEditor.PreviewManager.PreviewBoundSettings(player.transform, MaxCastDistance));
        }
        protected virtual void OnAbilActivate(Vector2 castPos, T player)
        {
            ResetCooldown();
            Editor.Editor.Instance.PlayerSpace.TrySpendMana(BaseManacost);
            OnActivate(castPos, player);
            OnAbilityActivate?.Invoke();
        }
        protected virtual void OnActivate(Vector2 castPos, T player)
        {

        }
    }
}
