using System;
using System.Collections;
using General.Interfaces;
using UnityEngine;

namespace Controls
{
    public class PlayerMovement : Movement, IKnockBackable
    {
        [SerializeField] private CameraController cameraController;

        private AfterImage _afterImage;

        private float _jumpTimeCounter;

        private bool _canDrop;

        protected override void Awake()
        {
            base.Awake();
            
            _afterImage = GetComponent<AfterImage>();
        }

        public void KnockBack(KnockBackData data)
        {
            data.angle.Normalize();
            Rigidbody.velocity = new Vector2(data.strength * data.angle.x * data.direction, data.strength * data.angle.y);
        }

        protected override void Update()
        {
            base.Update();
            
            DirX = Input.GetAxisRaw("Horizontal");
            DirY = Input.GetAxisRaw("Vertical");

            if (!isJumping && Ground)
            {
                cameraController.UnflipYOffset();
                Jumps = jumpAmount;

                if (!isDashing)
                {
                    CanDash = true;
                }
            }
            
            if (!canMove) return;
        
            SetVelocityX(MoveSpeed * DirX);

            if (Input.GetButtonDown("Drop") && _canDrop)
            {
                _canDrop = false;
                BoxCollider.isTrigger = true;
            }

            if (Input.GetButtonDown("Jump") && Jumps > 0)
            {
                _canDrop = false;
                _jumpTimeCounter = JumpTime;
                Jump(JumpForce);
            }

            if (Input.GetButton("Jump") && isJumping)
            {
                if (_jumpTimeCounter > 0f)
                {
                    SetVelocityY(JumpForce);
                    _jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
        
            if (Input.GetButtonDown("Dash") && CanDash)
            {
                _afterImage.Activate(true);
                Dash(DashingTime);
            }

            if (isDashing)
            {
                Rigidbody.velocity = DashingDirection.normalized * DashingVelocity;
            }

            UpdateAnimationState();
        }

        private void UpdateAnimationState()
        {
            if (!isAbilityDone) return;
        
            MovementState currentState;

            var facing = facingDirection;

            if (DirX > 0f)
            {
                facing = 1;
                currentState = MovementState.Running;
                cameraController.UnflipXOffset();
            }
            else if (DirX < 0f)
            {
                facing = -1;
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

            if (facing != facingDirection)
            {
                Flip();
            }

            
            // SpriteRenderer.flipX = facingDirection < 0f;
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
