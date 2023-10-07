using Assets.Scripts.Entities.Brain;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Projectiles.Mob.Boss;
using Assets.Scripts.Entities.Stats.StatAttributes;
using UnityEngine;

namespace Assets.Scripts.Entities.Mobs.Bosses.Lotus
{
	internal sealed class LotusBrain : EntityBrain<Lotus>
	{
		private float _nextSpecialTreshold;
		[Header("Attack")]
		[SerializeField] private float _minAttackBreak;
		[SerializeField] private float _maxAttackBreak;
		private float _currentAttackBreak;
		private float _timeSinceAttackBreak;
		[Header("Immediate Circle Strike")]
		[SerializeField] private LotusProjectile _bullet;
		[SerializeField] private byte _bulletCount = 4;
		[Header("Sin Circle Strike")]
		[SerializeField] private float _bulletPerSecond = 1;
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private float _strikeDuration;
		private float _timeSinceCircleStrikeCast;
		private float _timeSinceCircleStrike;
		private float _timeToSinShoot;
		private float _sinTime;
		private void Start()
		{
			_timeToSinShoot = 1 / _bulletPerSecond;
		}
		public override void OnReset()
		{
			_currentAttackBreak = _maxAttackBreak;
			//_nextSpecialTreshold = 1 - _specialAttackPerHealthPercentLost;
			_sinTime = 0.0f;
		}
		private void OnLotusDamaged(int oldHealth, int newHealth, Entity dealer)
		{
			if(newHealth <= Entity.Stats.GetValueInt<MaxHealthStat>() * _nextSpecialTreshold)
			{
				CastSpecial();
			}
		}
		protected override void RuntimeUpdate()
		{
			if(NavigationUtil.GetClosestEntityOfType(Entity.TargetType, Entity)  != null)
			{
				UpdateSelectTimer();
				UpdateSinAttack();
			}
		}
		private void UpdateSelectTimer()
		{
			if (_timeSinceAttackBreak < _currentAttackBreak)
			{
				_timeSinceAttackBreak += Time.deltaTime;
			}
			else SelectAttack();
		}
		private void SelectAttack()
		{
			_currentAttackBreak = UnityEngine.Random.Range(_minAttackBreak, _maxAttackBreak);
			if (UnityEngine.Random.Range(0, 1.0f) <= 0.5f)
			{
				StartSinAttack();
			}
			else ShootCircleImmediately();
			_timeSinceAttackBreak = 0.0f;
		}
		#region Attacks
		private void StartSinAttack()
		{
			_sinTime = 0.0f;
			_timeSinceCircleStrikeCast = 0.0f;
		}
		private void UpdateSinAttack()
		{
			if(_timeSinceCircleStrikeCast < _strikeDuration)
			{
				_sinTime += Time.deltaTime * _rotationSpeed;
				if(_timeSinceCircleStrike < _timeToSinShoot)
				{
					_timeSinceCircleStrike += Time.deltaTime;
				}
				else
				{
					SinShoot();
					_timeSinceCircleStrike = 0.0f;
				}
				_timeSinceCircleStrikeCast += Time.deltaTime;
			}
		}
		private void SinShoot()
		{
			var projectile = Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, _sinTime));
			projectile.Owner = Entity;
			projectile.SetMoveDirection(projectile.transform.up);
		}
		private void CastSpecial()
		{
			//_nextSpecialTreshold -= _specialAttackPerHealthPercentLost;
			ShootCircleImmediately();
		}
		private void ShootCircleImmediately()
		{
			for(int i = 0; i < 360; i += 360/_bulletCount)
			{
				var projectile = Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, i));
				projectile.SetMoveDirection(projectile.transform.up);
				projectile.Owner = Entity;
			}
		}
#endregion
		private void OnEnable() => Entity.OnHealthChanged += OnLotusDamaged;
		private void OnDisable() => Entity.OnHealthChanged -= OnLotusDamaged;
	}
}
