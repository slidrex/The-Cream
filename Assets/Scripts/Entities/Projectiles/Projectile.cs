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
        [field: SerializeField] public float Speed { get; private set; }
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _lifetime);
        }
        public void SetMoveDirection(Vector2 direction)
        {
            rb.velocity = direction.normalized * Speed;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<Entity>(out var entity) && entity.ThisType.MatchesTag(TriggerEntityType))
            {
                OnTrigger(entity);
            }
        }
        protected virtual void OnTrigger(Entity trigger)
        {

        }
    }
}
