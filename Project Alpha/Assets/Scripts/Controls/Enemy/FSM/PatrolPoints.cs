using System.Collections.Generic;
using UnityEngine;

namespace Controls.Enemy.FSM
{
    public class PatrolPoints : MonoBehaviour
    {
        public List<Vector3> points;
        private Vector3 _targetPoint;
        private int _index;
        
        private void Start()
        {
            _index = 0;
            _targetPoint = points[_index];
        }
        
        public bool HasReachedPoint()
        {
            return Vector3.Distance(transform.position, _targetPoint) <= 0.05f;
        }
        
        public void SetNextTargetPoint()
        {
            _index = _index == points.Count - 1 ? 0 : _index + 1;
            _targetPoint = points[_index];
        }
        
        public int GetTargetPointDirection()
        {
            return _targetPoint.x > transform.position.x ? 1 : -1;
        }
    }
}