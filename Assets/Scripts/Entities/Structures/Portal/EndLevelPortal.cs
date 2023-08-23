using Assets.Scripts.CompositeRoots;
using Assets.Scripts.Entities.Navigation.EntityType;
using Assets.Scripts.Entities.Stats.StatAttributes;
using Assets.Scripts.GameProgress;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Entities.Structures.Portal
{
    internal class EndLevelPortal : BaseEntity, IPlayerSelectTrigger
    {
        public Action OnActivateAction;
        public void OnSelect()
        {
            LevelCompositeRoot.Instance.BootStrapper.EndGame();
            OnActivateAction.Invoke();
            Destroy(gameObject);
        }
    }
}
