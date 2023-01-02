using System;
using System.Collections;
using System.Collections.Generic;
using General.Interfaces;
using General.Utilities;
using Powerup.Effects;
using UnityEngine;

namespace Powerup
{
    public class Powerup : Item, IInteractable
    {
        [field: SerializeField] public PowerupDataSO Data { get; private set; }
        
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;
        
        private bool _hasInteracted;

        public event Action<GameObject> OnPowerupPickup;

        public void CreatePowerup(PowerupDataSO data)
        {
            Data = data;
            var comps = gameObject.AddDependenciesToGO<PowerupComponent>(Data.GetAllDependencies());
            comps.ForEach(item => item.SetReferences());
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();

            CreatePowerup(Data);
        }

        public bool HasInteracted()
        {
            return _hasInteracted;
        }

        public void Interact(GameObject player)
        {
            if (_hasInteracted)
            {
                return;
            }

            // Hide component
            _spriteRenderer.enabled = false;
            _collider.enabled = false;

            // Get inventory
            // var inventory = player.GetComponent<Inventory>();
        
            // Apply powerups to player
            OnPowerupPickup?.Invoke(player);

            _hasInteracted = true;
        }
    }
}