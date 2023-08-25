using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Training.Highlight
{
	internal class HighlightController : MonoBehaviour
	{
		[SerializeField] private Image _fade;
		private GameObject[] _elements;
		private HighlightEntity[] _highlightedEntities;
		public struct HighlightEntity
		{
			public SpriteRenderer Renderer;
			public string SortingLayer;
			public int OrderInLayer;

			public HighlightEntity(SpriteRenderer renderer)
			{
				Renderer = renderer;
				SortingLayer = renderer.sortingLayerName;
				OrderInLayer = renderer.sortingOrder;
			}
		}
		public void HighlightElements(bool fadeIsRaycastTarget = false, params GameObject[] elements)
		{
			_fade.raycastTarget = fadeIsRaycastTarget;
			if (_elements != null)
			{
				HidePreviousElements();
			}
			_fade.gameObject.SetActive(true);
			_elements = elements;
			SetElementsSiblingIndex(true);
		}
		public void HighlightEntities(IEnumerable<SpriteRenderer> elements)
		{
			_fade.gameObject.SetActive(true);
			_highlightedEntities = new HighlightEntity[elements.Count()];
			int i = 0;
			foreach (var element in elements)
			{
				_highlightedEntities[i] = new HighlightEntity(element);
				element.sortingLayerName = "Top UI";
				i++;
			}
		}
		public void UnhighlightEntities()
		{
			_fade.gameObject.SetActive(false);
			for (int i = 0; i < _highlightedEntities.Length; i++)
			{
				_highlightedEntities[i].Renderer.sortingOrder = _highlightedEntities[i].OrderInLayer;
				_highlightedEntities[i].Renderer.sortingLayerName = _highlightedEntities[i].SortingLayer;
			}
			_highlightedEntities = null;
		}
		public void UnhighlightElements()
		{
			if (_elements != null)
				HidePreviousElements();
			_elements = null;
		}
		private void HidePreviousElements()
		{
			SetElementsSiblingIndex(false);
		}
		private void SetElementsSiblingIndex(bool aboveFade)
		{
			foreach (var element in _elements)
			{
				if(aboveFade) element.transform.SetAsLastSibling();
				else element.transform.SetAsFirstSibling();
			}
		}
	}
}
