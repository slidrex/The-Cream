using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Util.Events.EventAlert
{
	internal sealed class AlertUtil
	{
        private readonly Image _alertImage;
        public AlertUtil(Sprite alertImage, Transform container, float yOffset, float imageScale)
        {
            _alertImage = new GameObject().AddComponent<Image>();
			var canvas = new GameObject().AddComponent<Canvas>();
            canvas.sortingLayerID = SortingLayer.NameToID("Player");
            canvas.sortingOrder = 1;
            canvas.transform.parent = container.transform;
            _alertImage.transform.parent = canvas.transform;
            canvas.transform.localScale = 0.02f * Vector2.one;
            canvas.transform.localPosition = Vector2.zero;
            _alertImage.transform.localPosition = Vector2.zero + 100 * yOffset * Vector2.up;
            _alertImage.sprite = alertImage;
            _alertImage.rectTransform.sizeDelta = 100 * imageScale * Vector2.one;
            EnableMark(false);
        }
        public void EnableMark(bool active)
        {
            _alertImage.gameObject.SetActive(active);
        }
    }
}
