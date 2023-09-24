using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Navigation.Util;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Stats.Interfaces.Stats;
using Assets.Scripts.Entities.Templates;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sunstrike : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private GameObject damageParticles;
    private int mobDamage = 5;
    private int playerDamage = 25;
    private ChaseMob owner;
    
    internal void Init(ChaseMob owner)
    {
        this.owner = owner;
    }

    private void DestroySunstrike()
    {
        Destroy(gameObject);
    }

    public void BurnEverything()
    {
        List<Entity> playerEntities = NavigationUtil.GetAllEntitiesOfType(new EntityType<PlayerTag>().Any(), transform, radius);
        List<Entity> mobEntities = NavigationUtil.GetAllEntitiesOfType(new EntityType<MobTag>().Any(), transform, radius);
        List<Entity> entities = new List<Entity>(playerEntities);
        entities.AddRange(mobEntities);

        if (entities.Count > 0)
        {
            foreach (Entity entity in entities)
            {
                Destroy(Instantiate(damageParticles, entity.transform.position, Quaternion.identity), 2);
                if(entity is Player player)
                    (player as IDamageable).Damage(playerDamage, owner);
                else
                    (entity as IDamageable).Damage(mobDamage, owner);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
