using System;
using Controls;
using UnityEngine;

namespace General.Finite_State_Machine
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState initialState;
        public BaseState CurrentState { get; set; }
        
        public Movement Movement { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        private void Awake()
        {
            Movement = GetComponent<Movement>();
            Rigidbody = Movement.Rigidbody;
            Animator = Movement.Animator;
            
            CurrentState = initialState;
        }

        private void Start()
        {
            CurrentState.Enter(this);
        }

        private void Update()
        {
            CurrentState.Execute(this);
        }
    }
}