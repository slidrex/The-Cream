using Assets.Scripts.Entities.Mobs;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Stats.Interfaces.Templates;
using Assets.Scripts.Entities.Stats.Strategies;
using Assets.Scripts.Environment;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.Scripts.Entities.Templates
{
    internal abstract class ChaseMob : Mob, IBaseMobStatsProvider, IDamageable
    {
        [SerializeField] private ParticleSystem onDamageParticles;
        private SpriteRenderer spriteRenderer;
        public override EntityTypeBase ThisType => new EntityType<MobTag>(MobTag.AGGRESSIVE);
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }

        public void Damage(int damage, Entity deler)
        {
            EntityHealthStrategy.Damage(this, damage, deler);
            OnDamage(deler);
        }
        public virtual void OnDamage(Entity deler)
        {
            if(onDamageParticles != null)
                onDamageParticles.Play();

            StartCoroutine(ChangeColor());
        }
        private IEnumerator ChangeColor()
        {
            Color32 initialColor = spriteRenderer.color;

            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(0.2f);

            spriteRenderer.color = initialColor;
        }

        public void Heal(int heal)
        {
            EntityHealthStrategy.Heal(this, heal);
        }

        public virtual void OnDie()
        {
            if (onDamageParticles != null)
            {
                var main = onDamageParticles.main;
                var velocity = onDamageParticles.velocityOverLifetime;
                main.playOnAwake = true;
                main.startColor = Color.red;
                velocity.radial = 1;
                ParticlesUtil.SpawnParticles(onDamageParticles.gameObject, transform);
            }
            Destroy(gameObject);
        }

        private void Start()
        {
            EntityHealthStrategy.ResetHealth(this);
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
