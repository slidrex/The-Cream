using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Util.Config.Input
{
    internal class InputConfig
    {
        public Dictionary<ActionKey, KeyCode> Keys = new Dictionary<ActionKey, KeyCode>()
        {
            [ActionKey.FIRST_HERO_ABILITY] = KeyCode.Q,
            [ActionKey.SECOND_HERO_ABILITY] = KeyCode.W,
            [ActionKey.THIRD_HERO_ABILITY] = KeyCode.E,

            [ActionKey.FIRST_RUNTIME_ABILITY] = KeyCode.Z,
            [ActionKey.SECOND_RUNTIME_ABILITY] = KeyCode.X,
            [ActionKey.THIRD_RUNTIME_ABILITY] = KeyCode.C,
            [ActionKey.FOURTH_RUNTIME_ABILITY] = KeyCode.V,
            [ActionKey.FIVETH_RUNTIME_ABILITY] = KeyCode.B,
            [ActionKey.SIXTH_RUNTIME_ABILITY] = KeyCode.N,
    };
        public void SetKey(ActionKey key, KeyCode value)
        {
            for(int i = 0; i < Keys.Count; i++)
            {
                var k = Keys.ElementAt(i);
                if (k.Value == value) Keys[k.Key] = KeyCode.None;
            }
            Keys[key] = value;
        }
        public enum ActionKey
        {
            FIRST_HERO_ABILITY,
            SECOND_HERO_ABILITY,
            THIRD_HERO_ABILITY,

            FIRST_RUNTIME_ABILITY,
            SECOND_RUNTIME_ABILITY,
            THIRD_RUNTIME_ABILITY,
            FOURTH_RUNTIME_ABILITY,
            FIVETH_RUNTIME_ABILITY,
            SIXTH_RUNTIME_ABILITY
        }
    }
}
