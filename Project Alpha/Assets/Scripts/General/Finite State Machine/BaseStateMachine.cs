using System;
using System.Collections.Generic;
using Combat;
using Combat.Enemy;
using Controls;
using General.Utilities;
using Unity.VisualScripting;
using UnityEngine;

namespace General.Finite_State_Machine
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private List<BaseState> initialStates;
        [SerializeField] public List<BaseState> currentStates;
        public SortedDictionary<string, float> Timers;

        public bool isDead;
        
        public Movement Movement { get; private set; }
        public EnemyHealth Health { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public Collider2D Collider { get; private set; }
        public Animator Animator { get; private set; }
        public GameObject Player { get; private set; }

        public float dirX;
        public float dirY;

        private void Awake()
        {
            Movement = GetComponent<Movement>();
            Collider = GetComponent<Collider2D>();
            Health = GetComponent<EnemyHealth>();
            Player = GameObject.FindWithTag("Player");
            currentStates = initialStates;
            Timers = new SortedDictionary<string, float>();
        }

        private void Start()
        {
            Rigidbody = Movement.Rigidbody;
            Animator = Movement.Animator;
            
            for(var index = 0; index < currentStates.Count; index++)
            {
                currentStates[index].Enter(this);
            }
        }

        private void Die()
        {
            Rigidbody.bodyType = RigidbodyType2D.Static;
            Collider.enabled = false;
            Animator.SetBool("isDead", true);
        }

        private void OnEnable()
        {
            Health.OnDeath += Die;
        }

        private void OnDisable()
        {
            Health.OnDeath -= Die;
        }

        private void Update()
        {
            if (isDead) return;
            
            var keysToRemove = new List<string>();
            var keys = new List<string>(Timers.Keys);
            
            foreach(var key in keys)
            {
                Timers[key] -= Time.deltaTime;

                if (Timers[key] <= 0f)
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (var key in keysToRemove)
            {
                Timers.Remove(key);
            }
            
            for(var index = 0; index < currentStates.Count; index++)
            {
                currentStates[index].Execute(this, index);
            }
        }

        public void AddToTimers(string key, float cooldown)
        {
            Timers.Add(key, cooldown);
        }
    }
}