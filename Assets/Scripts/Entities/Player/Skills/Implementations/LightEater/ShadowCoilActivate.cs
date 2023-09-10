using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Implementations.LightEater
{
    public class ShadowCoilActivate : MonoBehaviour
    {
        [SerializeField] private string animationTrigger = "CoilAttack";
        private Animator _animator;
        private Vector2 mousePosition;
        private CoilProjectile _coil;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        internal void CoilStart(Vector2 mousePos, CoilProjectile coil)
        {
            _animator.SetTrigger(animationTrigger);
            mousePosition = mousePos;
            _coil = coil;
        }
        public void InstatiateCoil()
        {
            GameObject coil = Instantiate(_coil.gameObject, mousePosition, Quaternion.identity);
            Destroy(coil.gameObject, 0.4f);
        }
    }
}
