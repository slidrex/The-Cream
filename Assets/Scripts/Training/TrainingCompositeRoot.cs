using Assets.Scripts.Training.Highlight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Training
{
	internal class TrainingCompositeRoot : MonoBehaviour
	{
		public static TrainingCompositeRoot Instance { get; private set; }
		public HighlightController HighlightController;
		private void Awake()
		{
			Instance = this;
		}
	}
}
