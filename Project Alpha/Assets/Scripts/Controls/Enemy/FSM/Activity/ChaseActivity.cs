using General.Finite_State_Machine;
using UnityEngine;

namespace Controls.Enemy.FSM.Activity
{
    [CreateAssetMenu(menuName = "AI/FSM/Activity/ChaseActivity")]
    public class ChaseActivity : General.Finite_State_Machine.Activity
    {
        private GameObject _target;
        public string targetTag;
        public float speedMultiplier = 1;

        private void GetTarget(BaseStateMachine stateMachine)
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
 
        public override void Enter(BaseStateMachine stateMachine)
        {
            GetTarget(stateMachine);
        }
 
        public override void Execute(BaseStateMachine stateMachine)
        {
            var movement = stateMachine.Movement;
            
            var dir = _target.transform.position.x > stateMachine.transform.position.x ? 1 : -1;

            movement.SetVelocityX(movement.MoveSpeed * speedMultiplier * dir);
            stateMachine.dirX = dir;

            // if (movement.canFlip)
            // {
            //     if(movement.canFlip && dir != movement.facingDirection) movement.Flip();
            // }
            }
 
        public override void Exit(BaseStateMachine stateMachine)
        {
            
        }
    }
}