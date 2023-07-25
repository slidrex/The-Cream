using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Projectiles.Character.Bristleback
{
    internal class Bristlespike : Projectile
    {
        private Player.Player _player;
        [UnityEngine.SerializeField] private int damage;
        public void SetOwner(Player.Player player)
        {
            _player = player;
        }
        public override EntityTypeBase TriggerEntityType => new EntityType<MobTag>().Any();
        protected override void OnTrigger(Entity trigger)
        {
            (trigger as IDamageable).Damage(damage, _player);
        }
    }
}
