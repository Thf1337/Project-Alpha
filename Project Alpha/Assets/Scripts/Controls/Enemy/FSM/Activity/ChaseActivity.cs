﻿using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Activity
{
    [CreateAssetMenu(menuName = "AI/FSM/Activity/ChaseActivity")]
    public class ChaseActivity : General.Finite_State_Machine.Activity
    {
        private GameObject _target;
        public string targetTag;
        public float speed = 1;
 
        public override void Enter(BaseStateMachine stateMachine)
        {
            if (targetTag == "Player")
            {
                _target = stateMachine.Player;
            }
            else
            {
                _target = GameObject.FindWithTag(targetTag);
            }
        }
 
        public override void Execute(BaseStateMachine stateMachine)
        {
            var movement = stateMachine.Movement;
            
            if (movement.canMove)
            {
                var dir = _target.transform.position.x > stateMachine.transform.position.x ? 1 : -1;

                movement.SetVelocityX(speed * dir);
                movement.Animator.SetInteger("currentState", (int)States.Running);

                if (movement.canFlip)
                {
                    if(movement.canFlip && dir != movement.facingDirection) movement.Flip();
                }
            }
        }
 
        public override void Exit(BaseStateMachine stateMachine)
        {
        }
    }
}