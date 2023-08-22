using UnityEngine;

namespace Assets.Scripts.Entities.Player.Skills.Implementations.LightEater
{
    public class ShadowCoilActivate : MonoBehaviour
    {
        [SerializeField] private GameObject _coil;
        [SerializeField] private string animationTrigger = "CoilAttack";
        private Animator _animator;
        private Vector2 mousePosition;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        internal void CoilStart(Vector2 mousePos)
        {
            _animator.SetTrigger(animationTrigger);
            mousePosition = mousePos;
        }
        public void InstatiateCoil()
        {
            Instantiate(_coil, mousePosition, Quaternion.identity);
        }
    }
}
