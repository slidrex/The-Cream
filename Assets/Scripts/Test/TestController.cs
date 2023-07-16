using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Test
{
    internal class TestController : MonoBehaviour
    {
        [SerializeField] private GameObject _anim;
        private Player _player;
        private void Awake()
        {
            _player = FindObjectOfType<Player>();
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                LevelCompositeRoot.Instance.Runner.RunLevel();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Instantiate(_anim, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            }
        }
    }
}
