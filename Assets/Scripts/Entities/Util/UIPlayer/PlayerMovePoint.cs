using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Util.UIPlayer
{
    internal class PlayerMovePoint : MonoBehaviour
    {
        public string APPEAR_TRIGGER { get; private set; } = "Appear";
		private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public Animator GetAnimator() { return animator; }
    }
}
