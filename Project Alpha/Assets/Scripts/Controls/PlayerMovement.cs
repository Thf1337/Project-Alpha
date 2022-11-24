using System.Collections;
using UnityEngine;

namespace Controls
{
    public class PlayerMovement : Movement
    {
        [Header("Jumping")]
        public float jumpTimeMultiplier;

        [SerializeField] private CameraController cameraController;

        private TrailRenderer _trailRenderer;
    
        private float _jumpTimeCounter;
    
        protected override void Start()
        {
            base.Start();
            
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            if (!canMove) return;
            
            var moveSpeed = baseMoveSpeed + baseMoveSpeed * moveSpeedMultiplier;
            var jumpForce = baseJumpForce + baseJumpForce * jumpForceMultiplier;
            var jumpTime = baseJumpTime + baseJumpTime * jumpTimeMultiplier;
            var dashingVelocity = baseDashingVelocity + baseDashingVelocity * dashingVelocityMultiplier;
            var dashingTime = baseDashingTime + baseDashingTime * dashingTimeMultiplier;
        
            DirX = Input.GetAxisRaw("Horizontal");
            DirY = Input.GetAxisRaw("Vertical");
        
            SetVelocityX(moveSpeed * DirX);

            if (Input.GetButtonDown("Jump") && Jumps > 0) 
            {
                _jumpTimeCounter = jumpTime;
                Jump(jumpForce);
            }

            if (Input.GetButton("Jump") && IsJumping)
            {
                if (_jumpTimeCounter > 0f)
                {
                    SetVelocityY(jumpForce);
                    _jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    IsJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                IsJumping = false;
            }
        
            if (Input.GetButtonDown("Dash") && CanDash)
            {
                _trailRenderer.emitting = true;
                Dash(dashingTime);
            }

            if (isDashing)
            {
                Rigidbody.velocity = DashingDirection.normalized * dashingVelocity;
            }

            if (Jumps == jumpAmount)
            {
                CanDash = true;
            }

            UpdateAnimationState();
        }

        private void UpdateAnimationState()
        {
            if (!isAbilityDone) return;
        
            MovementState currentState;

            if (DirX > 0f)
            {
                facingDirection = 1;
                currentState = MovementState.Running;
                cameraController.UnflipXOffset();
            }
            else if (DirX < 0f)
            {
                facingDirection = -1;
                currentState = MovementState.Running;
                cameraController.FlipXOffset();
            }
            else
            {
                currentState = MovementState.Idle;
            }

            if (Rigidbody.velocity.y > .1f)
            {
                currentState = MovementState.Jumping;
                cameraController.UnflipYOffset();
            }
            else if (Rigidbody.velocity.y < -.1f)
            {
                currentState = MovementState.Falling;
                cameraController.FlipYOffset();
            }

            SpriteRenderer.flipX = facingDirection < 0f;
            Animator.SetInteger(CurrentState, (int) currentState);
        }

        protected override IEnumerator StopDashing(float dashingTime)
        {
            yield return base.StopDashing(dashingTime);

            _trailRenderer.emitting = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Jumps == jumpAmount)
            {
                cameraController.UnflipYOffset();
                return;
            }

            if (!collision.gameObject.CompareTag("Ground"))
            {
                return;
            }
        
            foreach (ContactPoint2D hitPosition in collision.contacts)
            {
                // Check if its collided on top 
                if (hitPosition.normal.x != 0 && hitPosition.normal.y > 0)
                {
                    cameraController.UnflipYOffset();
                    Jumps = jumpAmount;
                }
            }
        }
    }
}
