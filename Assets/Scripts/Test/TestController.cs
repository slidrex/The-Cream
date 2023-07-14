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
            if(Input.GetKeyDown(KeyCode.Z))
            {
                var instantDamageMod = new InstantDamage(_player, 0.3f);
                _player.StatModifierHandler.AddModifier(instantDamageMod);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                var instantHealthMod = new InstantHeal(_player, 0.2f);
                _player.StatModifierHandler.AddModifier(instantHealthMod);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                var poison = new Poison(_player, 10, 6, 0.3f);
                _player.StatModifierHandler.AddModifier(poison);
            }
        }
    }
}
