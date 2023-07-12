using Assets.Scripts.Entities.Movement.Player;
using Assets.Scripts.Entities.Navigation.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Navigation.Player
{
    internal class PlayerPullableChaseMovement : PullableChaseMovement<MobTag>
    {
        protected override EntityType<MobTag> _targetTypes => new EntityType<MobTag>().Any();
    }
}
