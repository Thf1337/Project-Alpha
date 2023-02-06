using System;
using System.Collections;
using Combat;
using General.Interfaces;
using General.Utilities;
using UnityEngine;

namespace Controls
{
    public class Movement : MonoBehaviour, IKnockBackable
    {
        [Header("Movement variables")]
        public float baseMoveSpeed;
        public float moveSpeedMultiplier;
        public int facingDirection = 1;
        public bool isAbilityDone;
        public bool canMove;
        public bool canFlip;
        [SerializeField] protected bool isKnockBackAble;
        
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
    
        public Rigidbody2D Rigidbody { get; private set; }
        public Health Health { get; private set; }
        public BoxCollider2D BoxCollider { get; private set; }
        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        protected int Jumps;
        public bool isJumping;
        protected float DirX;
        protected float DirY;

        private Timer _knockBackTimer;
        public float invincibilityDuration = 0.5f;

        protected static readonly int CurrentState = Animator.StringToHash("currentState");

        protected virtual void Awake()
        {
            canMove = true;
            isAbilityDone = true;
            Jumps = jumpAmount;
            _knockBackTimer = new Timer(invincibilityDuration);
        
            Rigidbody = GetComponent<Rigidbody2D>();
            Health = GetComponent<Health>();
            BoxCollider = GetComponent<BoxCollider2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void KnockBackDone()
        {
            canMove = true;
            _knockBackTimer.StopTimer();
        }

        protected virtual void OnEnable()
        {
            _knockBackTimer.OnTimerDone += KnockBackDone;
        }

        protected virtual void OnDisable()
        {
            _knockBackTimer.OnTimerDone -= KnockBackDone;
        }

        protected virtual void Update()
        {
            _knockBackTimer.Tick();
        }
        public virtual void Jump(float jumpForce)
        {
            SetVelocityY(jumpForce);
            isJumping = true;
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
            if (Health.isDead) return;
            
            angle.Normalize();
            Rigidbody.velocity = new Vector2(angle.x * velocity * direction, angle.y * velocity);
        }

        public void SetVelocityX(float velocity)
        {
            if(canMove && !Health.isDead) Rigidbody.velocity = new Vector2(velocity, Rigidbody.velocity.y);
        }

        public void SetVelocityY(float velocity)
        {
            if(canMove && !Health.isDead) Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, velocity);
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
            if (canFlip && DirX != 0f && (int) DirX != facingDirection)
            {
                Flip();
                return true;
            }

            return false;
        }

        public bool CheckJump()
        {
            if (isJumping && Ground)
            {
                isJumping = false;
                ResetJumps();
                return true;
            }

            return false;
        }

        public void Flip()
        {
            facingDirection *= -1;
            Rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        
        public virtual void KnockBack(KnockBackData data)
        {
            if (!isKnockBackAble || Health.isDead) return;

            canMove = false;
            
            data.angle.Normalize();
            Rigidbody.velocity = new Vector2(data.strength * data.angle.x * data.direction, data.strength * data.angle.y);
            
            _knockBackTimer.StartTimer();
        }
    }
}