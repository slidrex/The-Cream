using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Assets.Scripts.Training.ActionChain.Chains._2Tile
{
	internal class KillTwoMobsChain : TrainingActionChain
	{
		private int _killedCount;
		private Player _player;
		protected override void OnConfigure(Player player)
		{
			_player = player;
			player.OnKillCallback += OnKill;
		}
		private void OnKill()
		{
			if (_killedCount < 1) _killedCount++;
			else
			{
				_player.OnKillCallback -= OnKill;
				ConfirmChain();
			}
		}
	}
}
