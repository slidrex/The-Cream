using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.Util.Config.Input
{
    internal static class InputConfig
    {
        public static KeyCode FIRST_HERO_ABILITY { get; set; } = KeyCode.Q;
        public static KeyCode SECOND_HERO_ABILITY { get; set; } = KeyCode.W;
        public static KeyCode THIRD_HERO_ABILITY { get; set; } = KeyCode.E;

        public static KeyCode FIRST_RUNTIME_ABILITY { get; set; } = KeyCode.Z;
        public static KeyCode SECOND_RUNTIME_ABILITY { get; set; } = KeyCode.X;
        public static KeyCode THIRD_RUNTIME_ABILITY { get; set; } = KeyCode.C;
        public static KeyCode FOURTH_RUNTIME_ABILITY { get; set; } = KeyCode.V;
        public static KeyCode FIVETH_RUNTIME_ABILITY { get; set; } = KeyCode.B;
        public static KeyCode SIXTH_RUNTIME_ABILITY { get; set; } = KeyCode.N;
    }
}
