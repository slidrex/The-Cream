using Assets.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.LevelEditor.Ability
{
    internal class AbilityAdapter : MonoBehaviour
    {
        private Action<Vector2> _previewAction;
        public static AbilityAdapter Instance { get; private set; }
        private PreviewManager.PreviewBoundSettings _boundSettings;
        private bool _selectBlock;
        public PreviewManager.PreparationStage StartAbilityPreview(Action<Vector2> onPreviewAction, ObjectHolder holder, bool clickedByIcon = false, PreviewManager.PreviewBoundSettings boundSettings = null)
        {
            _previewAction = onPreviewAction;
            _boundSettings = boundSettings;

            var status = clickedByIcon == false ? Editor.Editor.Instance.PreviewManager.PerformAction(new PreviewManager.Config(onPreviewAction, holder) { Status = PreviewManager.PreviewStatus.AUTO }, boundSettings) : Editor.Editor.Instance.PreviewManager.PerformAction(new PreviewManager.Config(onPreviewAction, holder) { Status = PreviewManager.PreviewStatus.ENABLED }, boundSettings);

            return status;
        }
        private void Start()
        {
            Instance = this;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && _previewAction != null && EventSystem.current.IsPointerOverGameObject() == false)
            {
                if (_selectBlock)
                {
                    _selectBlock = false;
                    Editor.Editor.Instance.PreviewManager.PerformAction(new PreviewManager.Config(_previewAction, null) { Status = PreviewManager.PreviewStatus.ENABLED }, _boundSettings);
                }
                else
                {
                    Editor.Editor.Instance.PreviewManager.PerformAction(new PreviewManager.Config(_previewAction, null) { Status = PreviewManager.PreviewStatus.DISABLED }, _boundSettings);
                    _previewAction = null;
                    _boundSettings = null;
                }
            }
        }
    }
}
