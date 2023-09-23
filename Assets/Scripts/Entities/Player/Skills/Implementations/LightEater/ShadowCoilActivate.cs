using Assets.Scripts.Entities.Move;
using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Implementations.LightEater
{
    public class ShadowCoilActivate : MonoBehaviour
    {
        [SerializeField] private string animationTrigger = "CoilAttack";
        private Animator _animator;
        private Vector2 mousePosition;
        private CoilProjectile _coil;
        private Player _entity;
        private Movement _movement;

        private void Start()
        {
            _movement = GetComponent<Movement>();
            _animator = GetComponent<Animator>();
            _entity = GetComponent<Player>();
        }
        internal void CoilStart(Vector2 mousePos, CoilProjectile coil)
        {
            _entity.IsMuted = true;
            _movement.EnableMovement(false);
            _animator.SetTrigger(animationTrigger);
            mousePosition = mousePos;
            _coil = coil;
        }
        public void InstatiateCoil()
        {
            GameObject coil = Instantiate(_coil.gameObject, mousePosition, Quaternion.identity);
            Destroy(coil.gameObject, 0.4f);
            _entity.IsMuted = false;
            _movement.EnableMovement(true);
        }
    }
}
