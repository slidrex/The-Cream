using Assets.Scripts.Entities.Reset;
using Assets.Scripts.Entities.Util.UIPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LevelEditor.RuntimeSpace.PlayerUtil
{
	internal class PlayerMarks : MonoBehaviour, IResettable
	{
		private const float MOVE_POINT_STAY_TIME = 0.3f;
		private float _timeSincePlace;
		[SerializeField] private PlayerMovePoint _point;

		public void OnReset()
		{
			_point.gameObject.SetActive(false);
		}

		public void SetMovePoint(Vector2 position)
		{
			_point.gameObject.SetActive(true);
			_point.transform.position = position;
			_timeSincePlace = 0.0f;
		}
		private void Update()
		{
			if(_timeSincePlace < MOVE_POINT_STAY_TIME)
			{
				_timeSincePlace += Time.deltaTime;
				if(_timeSincePlace >= MOVE_POINT_STAY_TIME)
				{
					_point.gameObject.SetActive(false);
				}
			}
		}
	}
}
