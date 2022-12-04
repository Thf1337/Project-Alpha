using System;
using UnityEngine;

namespace Controls
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float followSpeedDefault = 0.7f;
        [SerializeField] private float followSpeedFast = 0.25f;
        [SerializeField] private float xOffset = 5f;
        [SerializeField] private float yOffsetUp = 0.5f;
        [SerializeField] private float yOffsetDown = -10f;
    
        [SerializeField] private float yOffsetLookUp = 5f;
        [SerializeField] private float yOffsetLookDown = -5f;

        [SerializeField] private Transform player;
        [SerializeField] private Rigidbody2D playerRigidbody;

        private bool _xOffsetFlipped;
        private bool _yOffsetFlipped;
        private Vector3 velocity = Vector3.zero;

        private void Start()
        {
            _xOffsetFlipped = false;
            _yOffsetFlipped = false;
        }

        private void FixedUpdate()
        {
            var playerPosition = player.position;
            var cameraPosition = transform.position;
            float yOffsetCamera;
            float xOffsetCamera;

            float look = Input.GetAxisRaw("Look");

            if (!_xOffsetFlipped && !_yOffsetFlipped)
            {
                xOffsetCamera = xOffset;
                yOffsetCamera = yOffsetUp;
            }
            else if (_xOffsetFlipped && !_yOffsetFlipped)
            {
                xOffsetCamera = -xOffset;
                yOffsetCamera = yOffsetUp;
            }
            else if (!_xOffsetFlipped && _yOffsetFlipped)
            {
                xOffsetCamera = xOffset;
                yOffsetCamera = yOffsetDown;
            }
            else
            {
                xOffsetCamera = -xOffset;
                yOffsetCamera = yOffsetDown;
            }

            var magnitude = playerRigidbody.velocity.magnitude;
            var followSpeed = followSpeedDefault;

            if (magnitude > 25.0f)
            {
                followSpeed = followSpeedFast;
            }

            if (look > 0.1f)
            {
                var lookUpPosition = new Vector3(playerPosition.x + xOffsetCamera, playerPosition.y + yOffsetLookUp, cameraPosition.z);
                transform.position =
                    Vector3.SmoothDamp(cameraPosition, lookUpPosition, ref velocity, followSpeed);
                return;

            }
        
            if(look < -0.1f)
            {
                var lookDownPosition = new Vector3(playerPosition.x + xOffsetCamera, playerPosition.y + yOffsetLookDown, cameraPosition.z);
                transform.position =
                    Vector3.SmoothDamp(cameraPosition, lookDownPosition, ref velocity, followSpeed);
                return;
            }
        
            var newPosition = new Vector3(playerPosition.x + xOffsetCamera, playerPosition.y + yOffsetCamera, cameraPosition.z);
            float distanceToPlayer = Vector2.Distance(cameraPosition, playerPosition);
            float maxDistance = _yOffsetFlipped ? Math.Abs(xOffsetCamera) + 7f : Math.Abs(xOffsetCamera) + yOffsetCamera + 5f;

            if (distanceToPlayer > maxDistance)
            {
                float fastFollowSpeed = Math.Max(maxDistance/distanceToPlayer - 0.85f, 0.05f);
                transform.position =
                    Vector3.SmoothDamp(cameraPosition, newPosition, ref velocity, fastFollowSpeed);
            }
            else
            {
                transform.position =
                    Vector3.SmoothDamp(cameraPosition, newPosition, ref velocity, followSpeed);
            }
        }

        public void FlipXOffset()
        {
            _xOffsetFlipped = true;
        }

        public void FlipYOffset()
        {
            _yOffsetFlipped = true;
        }

        public void UnflipXOffset()
        {
            _xOffsetFlipped = false;
        }

        public void UnflipYOffset()
        {
            _yOffsetFlipped = false;
        }
    }
}
