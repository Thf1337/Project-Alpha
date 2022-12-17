using System;
using System.Collections;
using UnityEngine;

namespace Controls
{
    public class PlayerMovement : Movement
    {
        [SerializeField] private CameraController cameraController;

        private AfterImage _afterImage;

        private float _jumpTimeCounter;

        private bool _canDrop;

        protected override void Start()
        {
            base.Start();
            
            _afterImage = GetComponent<AfterImage>();
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

            if (!IsJumping && Ground)
            {
                cameraController.UnflipYOffset();
                Jumps = jumpAmount;

                if (!isDashing)
                {
                    CanDash = true;
                }
            }

            if (Input.GetButtonDown("Drop") && _canDrop)
            {
                _canDrop = false;
                BoxCollider.isTrigger = true;
            }

            if (Input.GetButtonDown("Jump") && Jumps > 0)
            {
                _canDrop = false;
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

                _afterImage.Activate(true);
                Dash(dashingTime);
            }

            if (isDashing)
            {
                Rigidbody.velocity = DashingDirection.normalized * dashingVelocity;
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
                _canDrop = false;
                currentState = MovementState.Falling;
                cameraController.FlipYOffset();
            }

            SpriteRenderer.flipX = facingDirection < 0f;
            Animator.SetInteger(CurrentState, (int) currentState);
        }

        protected override IEnumerator StopDashing(float dashingTime)
        {
            yield return base.StopDashing(dashingTime);

            _afterImage.Activate(false);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            BoxCollider.isTrigger = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Platform"))
            {
                _canDrop = true;
            }
            else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
            {
                _canDrop = false;
            }
        }

        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     foreach (ContactPoint2D hitPosition in collision.contacts)
        //     {
        //         // Check if its collided on top 
        //         if (hitPosition.normal.x != 0 && hitPosition.normal.y > 0)
        //         {
        //             cameraController.UnflipYOffset();
        //             CanDash = true;
        //             Jumps = jumpAmount;
        //             return;
        //         }
        //     }
        // }
    }
}
