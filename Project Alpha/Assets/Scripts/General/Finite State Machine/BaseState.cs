using UnityEngine;

namespace General.Finite_State_Machine
{
    public class BaseState : ScriptableObject
    {
        public virtual void Enter(BaseStateMachine machine) { }
        
        public virtual void Execute(BaseStateMachine machine, int index) { }
        
        public virtual void Exit(BaseStateMachine machine) { } 
    }
}
