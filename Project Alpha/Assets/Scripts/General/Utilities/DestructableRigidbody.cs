using System;
using General.Interfaces;
using UnityEngine;

namespace General.Utilities
{
    public class DestructableRigidbody : MonoBehaviour
    {
        [SerializeField] private Vector2 direction;

        [SerializeField] private float torque;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            var randTorque = UnityEngine.Random.Range(-torque, torque) * 2;
            var randDirectionX = UnityEngine.Random.Range(direction.x - 50, direction.x + 50);
            var randDirectionY = UnityEngine.Random.Range(direction.y, direction.y * 2);
            var randDirection = new Vector2(randDirectionX, randDirectionY);
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(randDirection);
            _rigidbody.AddTorque(randTorque);
            
            Destroy(gameObject, UnityEngine.Random.Range(5f,8f));
        }
    }
}
