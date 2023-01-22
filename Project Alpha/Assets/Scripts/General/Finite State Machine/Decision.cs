using UnityEngine;

namespace General.Finite_State_Machine
{
    public abstract class Decision : ScriptableObject
    {
        public abstract void Enter(BaseStateMachine stateMachine);
        public abstract bool Decide(BaseStateMachine stateMachine);
    }
}