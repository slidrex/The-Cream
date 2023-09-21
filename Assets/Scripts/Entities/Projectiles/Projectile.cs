using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Projectiles
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    internal abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private float _lifetime;
        private Collider2D _collider;
        private Rigidbody2D rb;
        public abstract EntityTypeBase TriggerEntityType { get; }
        public Vector2 MoveVector { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _lifetime);
        }
        private void OnEnable()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged += OnGameModeChanged;
        }
        private void OnDisable()
        {
            LevelCompositeRoot.Instance.Runner.OnLevelModeChanged -= OnGameModeChanged;
        }
        public void SetMoveDirection(Vector2 direction)
        {
            MoveVector = direction.normalized;
            rb.velocity = MoveVector* Speed;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<Entity>(out var entity) && entity.ThisType.MatchesTag(TriggerEntityType))
            {
                OnTrigger(entity);
            }
        }
        private void OnGameModeChanged(Editor.GameMode mode)
        {
            if (mode != Editor.GameMode.RUNTIME && LevelCompositeRoot.Instance.Runner.PreviousGameMode == Editor.GameMode.RUNTIME) OnRuntimeRoundChanged(); 
        }
        protected virtual void OnRuntimeRoundChanged()
        {

        }
        protected virtual void OnTrigger(Entity trigger)
        {

        }

        public Collider2D GetCollider() => _collider;
        public float GetLifeTime() => _lifetime;
    }
}
