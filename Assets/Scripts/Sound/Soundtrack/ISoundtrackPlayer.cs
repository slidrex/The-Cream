using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Sound.Soundtrack
{
    internal interface ISoundtrackPlayer
    {
        void Stop();
        void Play(AudioClip clip);
    }
}
