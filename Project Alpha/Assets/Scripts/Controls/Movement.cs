using System.Collections;
using UnityEngine;

namespace Controls
{
    public class Movement : MonoBehaviour
    {
        [Header("Movement variables")]
        public float baseMoveSpeed;
        public float moveSpeedMultiplier;
        public int facingDirection;
        public bool isAbilityDone;
        public bool canMove;
    
        [Header("Jumping")]
        public float baseJumpForce;
        public float jumpForceMultiplier;
        public float baseJumpTime;
        public int jumpAmount;
    
        [Header("Dashing")]
        public Vector2 baseDashingVelocity;
        public float dashingVelocityMultiplier;
        public float baseDashingTime;
        public float dashingTimeMultiplier;
        public bool isDashing;
    
        protected Vector2 DashingDirection;
        protected bool CanDash = true;
        
        protected enum MovementState { Idle, Running, Jumping, Falling }
    
        protected Rigidbody2D Rigidbody;
        protected Animator Animator;
        protected SpriteRenderer SpriteRenderer;

        protected int Jumps;
        protected bool IsJumping;
        protected float DirX;
        protected float DirY;

        protected static readonly int CurrentState = Animator.StringToHash("currentState");
    
        protected virtual void Start()
        {
            canMove = true;
            isAbilityDone = true;
            Jumps = jumpAmount;
        
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        protected virtual void Jump(float jumpForce)
        {
            SetVelocityY(jumpForce);
            IsJumping = true;
            Jumps -= 1;
        }

        protected virtual void Dash(float dashingTime)
        {
            isDashing = true;
            CanDash = false;
            DashingDirection = new Vector2(DirX, DirY);

            if (DashingDirection == Vector2.zero)
            {
                DashingDirection = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine(StopDashing(dashingTime));
            
        }

        protected virtual IEnumerator StopDashing(float dashingTime)
        {
            yield return new WaitForSeconds(dashingTime);

            isDashing = false;
        }

        public void ResetJumps()
        {
            Jumps = jumpAmount;
        }

        public void SetVelocityX(float velocity)
        {
            Rigidbody.velocity = new Vector2(velocity, Rigidbody.velocity.y);
        }

        public void SetVelocityY(float velocity)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, velocity);
        }

        public bool CheckFlip()
        {
            if (DirX != 0f && DirX < facingDirection)
            {
                facingDirection *= -1;
                Flip();
                return true;
            }

            return false;
        }

        public void Flip()
        {
            Rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
}