using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities.Stats.Interfaces.States
{
	/// <summary>
	/// Indicates that entity can have immunity to damage.
	/// </summary>
	internal interface IInvulnerable
	{
		bool IsInvulnerable { get; }
	}
}
