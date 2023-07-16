using Assets.Scripts.Entities.Placeable;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Interfaces.Templates
{
    /// <summary>
    /// A base template stat interface that includes:
    /// 
    /// IDamageable
    /// IMoveable
    /// ICanAttack
    /// IPlaceable
    /// IEditorSpaceRequired
    /// 
    /// </summary>
    internal interface IBaseMobStatsProvider : IDamageable, ICanDamage, IMoveable, IPlaceable, IEditorSpaceRequired
    {
    }
}
