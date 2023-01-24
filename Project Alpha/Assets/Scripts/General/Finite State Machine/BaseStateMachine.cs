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
        public GameObject Player { get; private set; }

        private void Awake()
        {
            Movement = GetComponent<Movement>();
            Player = GameObject.FindWithTag("Player");
            CurrentState = initialState;
        }

        private void Start()
        {
            Rigidbody = Movement.Rigidbody;
            Animator = Movement.Animator;
            CurrentState.Enter(this);
        }

        private void Update()
        {
            CurrentState.Execute(this);
        }
    }
}