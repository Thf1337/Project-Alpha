using System;
using System.Collections;
using System.Collections.Generic;
using Combat.Player;
using General.Interfaces;
using General.Utilities;
using Powerup.Effects;
using UnityEngine;

namespace Powerup
{
    public class Powerup : Item, IInteractable
    {
        [field: SerializeField] public PowerupDataSO Data { get; private set; }
        
        public bool applyDirectly;
        
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        private bool _hasInteracted;

        public event Action<GameObject> OnPowerupPickup;

        public void CreatePowerup(PowerupDataSO data)
        {
            Data = data;
            var comps = gameObject.AddDependenciesToGO<PowerupComponent>(Data.GetAllDependencies());
            comps.ForEach(item => item.SetReferences());

            _spriteRenderer.sprite = data.PowerupSprite;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();

            try
            {
                CreatePowerup(Data);
            }
            catch (Exception e)
            {
                return;
            }
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

            if (!applyDirectly && Data.isPotion)
            {
                var potionScript = player.GetComponent<Potion>();
                potionScript.PickupPotion(Data);
            }
            else
            {
                // Apply powerups to player
                OnPowerupPickup?.Invoke(player);
            }
            
            _hasInteracted = true;
        }
    }
}