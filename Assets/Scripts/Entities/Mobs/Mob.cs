using Assets.Scripts.Entities.EntityExperienceLevel;
using Assets.Scripts.Sound.Entity;
using UnityEngine;

namespace Assets.Scripts.Entities.Mobs
{
    internal abstract class Mob : EditorConstructEntity, IExperienceGainer, IDieSoundPlayer
    {
		[field: SerializeField] public AudioClip OnDieSound { get; set; }
        public abstract int OnDieExp { get; }
    }
}
