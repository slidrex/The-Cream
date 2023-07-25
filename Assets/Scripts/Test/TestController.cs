using Assets.Editor;
using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.Entities.Stats.StatDecorators.Modifiers.Modifiers;
using Assets.Scripts.LevelEditor.RuntimeSpace.Player;
using Assets.Scripts.Stage;
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
        private StageController _cont;
        private Player _player;
        private void Awake()
        {
            Application.targetFrameRate = 60;
            _cont = FindObjectOfType<StageController>();
            _player = FindObjectOfType<Player>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Instantiate(_anim, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            }
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                Editor.Editor.Instance._levelActions.ActivateButton(ButtonType.MOVE_NEXT_LEVEL);
            }
        }
    }
}
