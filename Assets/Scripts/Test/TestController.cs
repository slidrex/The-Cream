using Assets.Scripts.CompositeRoots;
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
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                LevelCompositeRoot.Instance.Runner.RunLevel();
            }
        }
    }
}
