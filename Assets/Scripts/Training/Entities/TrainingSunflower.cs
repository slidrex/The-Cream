using Assets.Scripts.Entities.Mobs;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Training.Entities
{
	internal class TrainingSunflower : ChaseMob
	{
		public override int OnDieExp => 5;

		public override byte SpaceRequired => 5;

		public override EntityTypeBase ThisType => new EntityType<MobTag>(MobTag.AGGRESSIVE);

		public override EntityTypeBase TargetType => throw new NotImplementedException();
		public override void OnDie()
		{
			base.OnDie();
		}
	}
}
