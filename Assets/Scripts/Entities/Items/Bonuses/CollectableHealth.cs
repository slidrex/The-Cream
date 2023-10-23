using Assets.Scripts.Entities.Items.Collectable;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;

namespace Assets.Scripts.Entities.Items.Bonuses
{
	internal class CollectableHealth : CollectableItem
	{
		[UnityEngine.SerializeField] private int _minHealHealth, _maxHealHealth;
		protected override EntityTypeBase CollectEntityTypes => new EntityType<PlayerTag>().Any();

		protected override void OnCollect(Entity entity)
		{
			(entity as IDamageable).Heal(UnityEngine.Random.Range(_minHealHealth, _maxHealHealth));
		}
	}
}
