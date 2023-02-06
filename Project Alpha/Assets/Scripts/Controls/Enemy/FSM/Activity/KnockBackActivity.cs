using General.Finite_State_Machine;
using General.Interfaces;
using UnityEngine;

namespace Controls.Enemy.FSM.Activity
{
    [CreateAssetMenu(menuName = "AI/FSM/Activity/KnockBackActivity")]
    public class KnockBackActivity : General.Finite_State_Machine.Activity
    {
        public KnockBackData knockBackData;

        private Movement _playerMovement;
        
        public override void Enter(BaseStateMachine stateMachine)
        {
            knockBackData.source = stateMachine.gameObject;                                                                                                                                    
            knockBackData.direction = stateMachine.Player.transform.position.x >= stateMachine.transform.position.x ? 1 : -1;
            _playerMovement = stateMachine.Player.GetComponent<Movement>();
        }

        public override void Execute(BaseStateMachine stateMachine)
        {
            _playerMovement.KnockBack(knockBackData);
        }

        public override void Exit(BaseStateMachine stateMachine)
        {
            
        }
    }
}