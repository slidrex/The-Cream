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
        private void Start()
        {
            Instance = this;
            Editor.Editor.Instance._runtimeSystem.OnSelect = OnSelect;
        }
        private void OnSelect()
        {
            if(_selectBlock)
            {
                if(_previewAction != null)
                    EndAbilityPreview();
            }
            _selectBlock = true;
        }
        public void StartAbilityPreview(Action<Vector2> onPreviewAction)
        {
            Editor.Editor.Instance._runtimeSystem.Deselect();
            _selectBlock = false;
            _previewAction = onPreviewAction;
            Editor.Editor.Instance._inputManager.SetActivePreviewEntity(true);
        }
        private void EndAbilityPreview()
        {
            _previewAction = null;
        }
        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && _previewAction != null && EventSystem.current.IsPointerOverGameObject() == false)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _previewAction.Invoke(mousePos);
                Editor.Editor.Instance._runtimeSystem.Deselect();
                EndAbilityPreview();
            }
        }
    }
}
