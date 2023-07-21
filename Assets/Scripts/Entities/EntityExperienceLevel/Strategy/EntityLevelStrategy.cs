using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;

namespace Assets.Scripts.Entities.EntityExperienceLevel.Strategy
{
    internal class EntityLevelStrategy
    {
        public static void AddExperience(int exp, ILevelEntity levelEntity)
        {
            int nextLevelCost = levelEntity.GetLevelExperienceCost(levelEntity.CurrentLevel + 1);

            while(exp > 0)
            {
                if(levelEntity.CurrentExp + exp < nextLevelCost)
                {
                    levelEntity.CurrentExp += exp;
                    exp = 0;
                }
                else
                {
                    exp -= nextLevelCost;
                    levelEntity.CurrentExp = 0;
                    levelEntity.CurrentLevel++;
                    levelEntity.OnLevelUp();
                    nextLevelCost = levelEntity.GetLevelExperienceCost(levelEntity.CurrentLevel + 1);
                    levelEntity.AddExperience(0);
                }
            }
        }
        public static void ResetLevel(ILevelEntity levelEntity) 
        {
            levelEntity.CurrentLevel = 0;
            levelEntity.CurrentExp = 0;
            levelEntity.AddExperience(0);
        }
    }
}
