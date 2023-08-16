using Assets.Scripts.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    internal class PreviewManager : MonoBehaviour
    {
        private Config _currentAction;
        private PreviewBoundSettings _boundSettings;
        public enum PreparationStage
        {
            NONE,
            PREVIEW,
            ACTION
        }
        public enum PreviewStatus
        {
            AUTO,
            DISABLED,
            ENABLED
        }
        [Serializable]
        public class Config
        {
            public Action<Vector2> Action;
            public bool NotDeselectOnChoose;
            internal PreparationStage Stage;
            public ObjectHolder Holder;
            public PreviewStatus Status;
            public Config(Action<Vector2> action, ObjectHolder holder)
            {
                Stage = PreparationStage.NONE;
                Holder = holder;
                Action = action;
            }
        }
        public class PreviewBoundSettings
        {
            public PreviewBoundSettings(Transform boundTransform, float maxReachDistance)
            {
                BoundTransform = boundTransform;
                MaxReachDistance = maxReachDistance;
            }

            public Transform BoundTransform { get; private set; }
            public float MaxReachDistance { get; private set; }
            
        }
        public PreparationStage PerformAction(Config action, PreviewBoundSettings boundSettings = null)
        {
            _boundSettings = boundSettings;
            bool enablePreview = !ConfigManager.Instance.SettingsConfig.IsQuickcastEnabled;
            if (action.Status == PreviewStatus.ENABLED) enablePreview = true;
            else if(action.Status == PreviewStatus.DISABLED) enablePreview = false;

            if (enablePreview)
            {
                if(action == _currentAction && _currentAction.Stage == PreparationStage.PREVIEW)
                {
                    action.Stage = PreparationStage.ACTION;
                }
                else action.Stage = PreparationStage.PREVIEW;
            }
            else action.Stage = PreparationStage.ACTION;

            HandleAction(action);
            return action.Stage;
        }
        private void HandleAction(Config action)
        {
            if(_currentAction != null && _currentAction.Holder != null) _currentAction.Holder.SetActiveSelectImage(false);
            _currentAction = action;
            switch (action.Stage)
            {
                case PreparationStage.PREVIEW:
                    Editor.Editor.Instance._inputManager.SetActivePreviewEntity(true, null, _boundSettings);
                    break;
                case PreparationStage.ACTION:
                    Editor.Editor.Instance._inputManager.SetBound(_boundSettings);
                    action.Action.Invoke(GetCastPosition());
                    if(_currentAction.NotDeselectOnChoose == false)
                    {
                        Editor.Editor.Instance._inputManager.SetActivePreviewEntity(false, null, _boundSettings);
                        _currentAction = null;
                    }
                    else action.Stage = PreparationStage.PREVIEW;
                    break;
                case PreparationStage.NONE:
                    Editor.Editor.Instance._inputManager.SetActivePreviewEntity(false, null, _boundSettings);
                    _currentAction = null;
                    break;
            }
            if(action.Holder != null)
                action.Holder.SetActiveSelectImage(action.Stage == PreparationStage.PREVIEW);
        }
        private Vector2 GetCastPosition() => _boundSettings != null ? Editor.Editor.Instance._inputManager.GetPreviewEntityPosition() : Camera.main.ScreenToWorldPoint(Input.mousePosition);
        public void Deselect()
        {
            if (_currentAction != null && _currentAction.Holder != null) _currentAction.Holder.SetActiveSelectImage(false);

            _currentAction = null;
            Editor.Editor.Instance._inputManager.SetActivePreviewEntity(false, null, _boundSettings);
        }
    }
}
