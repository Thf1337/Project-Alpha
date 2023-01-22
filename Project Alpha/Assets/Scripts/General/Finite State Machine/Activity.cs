using UnityEngine;

namespace General.Finite_State_Machine
{
    public abstract class Activity : ScriptableObject
    {
        public abstract void Enter(BaseStateMachine stateMachine);
        
        public abstract void Execute(BaseStateMachine stateMachine);
        
        public abstract void Exit(BaseStateMachine stateMachine);
    }
}