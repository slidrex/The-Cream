using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities.RuntimeEntities.Animations
{
    internal class DropAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _onDestroyInstantiate;
        [SerializeField] private AnimationCurve _sizeCurve;
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        private Vector2 _destination;
        private float _maxSqrDist;
        private void Start()
        {
            _destination = transform.position;
            Camera mainCamera = Camera.main;

            float cameraHeight = mainCamera.orthographicSize * 2;
            Vector3 cameraPosition = mainCamera.transform.position;
            float topCameraBound = cameraPosition.y + (cameraHeight / 2);

            transform.position = new Vector3(transform.position.x, topCameraBound, transform.position.z);
            _maxSqrDist = Vector2.SqrMagnitude((Vector2)transform.position - _destination);
        }
        private void Update()
        {
            Vector2 pos = transform.position;
            transform.position = Vector2.MoveTowards(pos, _destination, _speed * Time.deltaTime);
            _speed += _acceleration * Time.deltaTime;
            float sqrDist = Vector2.SqrMagnitude((Vector2)pos - _destination);
            float dDistSqr = sqrDist / _maxSqrDist;
            float size = _sizeCurve.Evaluate(Mathf.Sqrt(dDistSqr));
            transform.localScale = size * Vector3.one;
            if (Mathf.Approximately(_destination.y, pos.y)) Destroy(gameObject); 
        }
        private void OnDestroy()
        {
            Instantiate(_onDestroyInstantiate, transform.position, Quaternion.identity);
        }
    }
}
