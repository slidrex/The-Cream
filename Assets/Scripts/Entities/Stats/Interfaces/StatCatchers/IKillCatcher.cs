using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Interfaces.StatCatchers
{
	internal interface IKillCatcher
	{
		Action OnKillCallback { get; set; }
		void OnKill();
	}
}
