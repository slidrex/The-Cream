using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.AI.ContextSteering
{
    internal class AIDataLoader : MonoBehaviour
    {
        [SerializeField] private float detectionRadius = 2;
        [SerializeField] private AIData aiData;
        private void Start()
        {
            aiData.currentTarget = FindObjectOfType<Player.Player>().transform;
        }
        private void Update()
        {
            aiData.obstacles = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        }
    }
}
