using System.Collections;
using General.Interfaces;
using Powerup.Effects;
using UnityEngine;

namespace Powerup
{
    [System.Serializable]
    public class Powerup : Item, IInteractable
    {
        // Specific powerup effect and its duration until it is reverted
        public PowerupEffect powerupEffect;
        public float powerupTime;

        private bool _hasInteracted = false;

        private IEnumerator RevertPowerup(GameObject player)
        {
            yield return new WaitForSeconds(powerupTime);
        
            powerupEffect.Revert(player);
        
            Destroy(gameObject);
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
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // Get inventory
            var inventory = player.GetComponent<Inventory>();
        
            // Apply powerup to player
            powerupEffect.Apply(player);
        
            // If the powerup is temporary, revert the effects afterwards
            if (powerupTime > 0f)
            {
                StartCoroutine(RevertPowerup(player));
            }
            else
            {
                // inventory.permanentPowerups.Add(powerupEffect);
                Destroy(gameObject);
            }
        }

        /*
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Only the player can pick up the powerup
            if (!collision.CompareTag("Player"))
            {
                return;
            }
            
            // Hide component
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            
            // Get player and his inventory
            var player = collision.gameObject;
            var inventory = player.GetComponent<Inventory>();
            
            // Apply powerup to player
            powerupEffect.Apply(player);
            
            // If the powerup is temporary, revert the effects afterwards
            if (powerupTime > 0f)
            {
                StartCoroutine(RevertPowerup(player));
            }
            else
            {
                // inventory.permanentPowerups.Add(powerupEffect);
                Destroy(gameObject);
            }
        }
        */
    }
}
