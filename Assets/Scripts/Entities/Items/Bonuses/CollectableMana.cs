using Assets.Scripts.Entities.Items.Collectable;
using Assets.Scripts.Entities.Navigation.EntityType;

namespace Assets.Scripts.Entities.Items.Bonuses
{
	internal class CollectableMana : CollectableItem
	{
		[UnityEngine.SerializeField] private int _minHealMana, _maxHealMana;
		protected override EntityTypeBase CollectEntityTypes => new EntityType<PlayerTag>().Any();

		protected override void OnCollect(Entity entity)
		{
			if(entity is Player.Player)
			{
				Editor.Editor.Instance.PlayerSpace.HealMana(UnityEngine.Random.Range(_minHealMana, _maxHealMana));
			}
		}
	}
}
