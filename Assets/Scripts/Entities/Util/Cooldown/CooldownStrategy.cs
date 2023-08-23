using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.Interfaces;
using Assets.Scripts.UI.PopupEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Util.Cooldown
{
    internal class CooldownStrategy
    {
        public static bool IsCooldownPassed(ICooldownable cooldownable, bool addErrorModelStateOnFail = true)
        {
            bool isCooldowned = cooldownable.TimeSinceActivation >= cooldownable.BaseCooldown;
            if (isCooldowned == false && addErrorModelStateOnFail) UIEventCompositeRoot.Instance.ErrorModel.AddError(ModelErrorMessages.ON_COOLDOWN);
            return isCooldowned;
        }
    }
}
