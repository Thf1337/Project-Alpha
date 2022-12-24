using System.Collections;
using UnityEngine;

namespace Controls
{
    public class Movement : MonoBehaviour
    {
        [Header("Movement variables")]
        public float baseMoveSpeed;
        public float moveSpeedMultiplier;
        public int facingDirection = 1;
        public bool isAbilityDone;
        public bool canMove;
        public bool canFlip;
        
        [Header("Jumping")]
        public float baseJumpForce;
        public float jumpForceMultiplier;
        public float baseJumpTime;
        public float jumpTimeMultiplier;
        public int jumpAmount;
    
        [Header("Dashing")]
        public Vector2 baseDashingVelocity;
        public float dashingVelocityMultiplier;
        public float baseDashingTime;
        public float dashingTimeMultiplier;
        public bool isDashing;
        
        [Header("Collision")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] protected float groundCheckRadius;
        [SerializeField] protected LayerMask groundLayerMask;

        public Transform GroundCheck
        {
            get => groundCheck;
            private set => groundCheck = value;
        }
        
        public bool Ground => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundLayerMask);

        public float BaseMoveSpeed => baseMoveSpeed / 10;

        public float GetStat(float baseStat, float multiplier) => baseStat + baseStat * multiplier;

        public float MoveSpeed => GetStat(BaseMoveSpeed, moveSpeedMultiplier);
        public float JumpForce => GetStat(baseJumpForce, jumpForceMultiplier);
        public float JumpTime => GetStat(baseJumpTime, jumpTimeMultiplier);
        public float DashingVelocityX => GetStat(baseDashingVelocity.x, dashingVelocityMultiplier);
        public float DashingVelocityY => GetStat(baseDashingVelocity.y, dashingVelocityMultiplier);
        public Vector2 DashingVelocity => new (DashingVelocityX, DashingVelocityY);
        public float DashingTime => GetStat(baseDashingTime, dashingTimeMultiplier);

        protected Vector2 DashingDirection;
        protected bool CanDash = true;
        
        protected enum MovementState { Idle, Running, Jumping, Falling }
    
        protected Rigidbody2D Rigidbody;
        protected BoxCollider2D BoxCollider;
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
            BoxCollider = GetComponent<BoxCollider2D>();
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

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            Rigidbody.velocity = new Vector2(angle.x * velocity * direction, angle.y * velocity);
        }

        public void SetVelocityX(float velocity)
        {
            Rigidbody.velocity = new Vector2(velocity, Rigidbody.velocity.y);
        }

        public void SetVelocityY(float velocity)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, velocity);
        }

        public void EnableFlip()
        {
            canFlip = true;
        }

        public void DisableFlip()
        {
            canFlip = false;
        }

        public bool CheckFlip()
        {
            if (canFlip && DirX != 0f && DirX != facingDirection)
            {
                Flip();
                return true;
            }

            return false;
        }

        public void Flip()
        {
            facingDirection *= -1;
            Rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
}