using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace General.Finite_State_Machine
{
    [CreateAssetMenu(menuName = "AI/FSM/State")]
    public sealed class State : BaseState
    {
        public List<Activity>   activities  = new List<Activity>();
        public List<Transition> transitions = new List<Transition>();

        public override void Enter(BaseStateMachine machine)
        {
            foreach (var activity in activities)
                activity.Enter(machine);
            
            foreach (var transition in transitions)
                transition.Enter(machine);
        }
        
        public override void Execute(BaseStateMachine machine, int index)
        {
            foreach (var activity in activities)
                activity.Execute(machine);
            
            foreach (var transition in transitions)
            {
                var changedState = transition.Execute(machine, index);

                if (changedState) return;
            }
        }
        
        public override void Exit(BaseStateMachine machine)
        {
            foreach (var activity in activities)
                activity.Exit(machine);
        }
    }
}