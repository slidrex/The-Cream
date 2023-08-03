using Assets.Editor;
using Assets.Scripts.LevelEditor.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Wrappers
{
    internal class PlayerDirectSkill : PlayerActiveSkill
    {
        public override bool TryActivate(SkillHolder holder, Player player)
        {
            Editor.Editor.Instance._runtimeSystem.Deselect();
            var playerSpace = Editor.Editor.Instance.PlayerSpace;

            if (TimeSinceActivation < BaseCooldown || !playerSpace.IsEnoughMana(BaseManacost)) return false;
            OnStartSelecting(holder, player);
            return true;
        }
        private void OnStartSelecting(SkillHolder holder, Player player)
        {
            AbilityAdapter.Instance.StartAbilityPreview((Vector2 mousePos) => OnAbilActivate(mousePos, player));
            Editor.Editor.Instance._runtimeSystem.SelectHolder(holder);
        }
        protected virtual void OnAbilActivate(Vector2 mousePos, Player player)
        {
            ResetTimer();
            Editor.Editor.Instance.PlayerSpace.TrySpendMana(BaseManacost);
            OnActivate(mousePos, player);
        }
        protected virtual void OnActivate(Vector2 mousePos, Player player)
        {

        }
    }
}
