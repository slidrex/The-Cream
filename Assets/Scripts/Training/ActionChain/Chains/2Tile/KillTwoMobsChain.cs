using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Stage;

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
