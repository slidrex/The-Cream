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
        private bool _selectBlock;
        public PreviewManager.PreparationStage StartAbilityPreview(Action<Vector2> onPreviewAction, ObjectHolder holder, bool clickedByIcon = false, PreviewManager.PreviewBoundSettings boundSettings = null)
        {
            _selectBlock = false;
            _previewAction = onPreviewAction;

            if (clickedByIcon == false)
                return Editor.Editor.Instance.PreviewManager.PerformAction(new PreviewManager.Config(onPreviewAction, holder), boundSettings);
            else return Editor.Editor.Instance.PreviewManager.PerformAction(new PreviewManager.Config(onPreviewAction, holder) { Status = PreviewManager.PreviewStatus.AUTO}, boundSettings);
        }
        private void Start()
        {
            Instance = this;
        }
        private void EndAbilityPreview()
        {
            _previewAction = null;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && _previewAction != null && EventSystem.current.IsPointerOverGameObject() == false)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _previewAction.Invoke(mousePos);
                EndAbilityPreview();
            }
        }
    }
}
