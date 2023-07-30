using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.EntityExperienceLevel
{
    internal interface ILevelEntity
    {
        byte CurrentLevel { get; set; }
        int CurrentExp { get; set; }
        int GetLevelExperienceCost(int level);
        void OnLevelUp();
        void AddExperience(int exp);
    }
}
