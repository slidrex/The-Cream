using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Player.Skills.Wrappers.Skill.Interfaces
{
    internal interface ICooldownable
    {
        float TimeSinceActivation { get; }
        float BaseCooldown { get; }
    }
}
