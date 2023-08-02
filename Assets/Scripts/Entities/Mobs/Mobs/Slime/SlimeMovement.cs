using Assets.Scripts.Entities.Attack;
using UnityEngine;

namespace Assets.Scripts.Entities.Move.Mobs.Slime
{
    internal class SlimeMovement : MobBaseAttack
    {
        private Animator animator;
        protected override void Start()
        {
            base.Start();
            animator = GetComponent<Animator>();
        }
        protected override void RuntimeUpdate()
        {
            base.RuntimeUpdate();
            //animator.SetInteger("moveX", Mathf.RoundToInt(Movement.MoveVector.normalized.x));
        }
        protected override void OnAttack(Entity target)
        {
            base.OnAttack(target);
            //animator.SetTrigger("Attack");
        }
    }

}
