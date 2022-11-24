using System.Collections;
using Powerup.Effects;
using UnityEngine;

namespace Powerup
{
    public class Powerup : MonoBehaviour
    {
        // Specific powerup effect and its duration until it is reverted
        public PowerupEffect powerupEffect;
        public float powerupTime;

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
                inventory.permanentPowerups.Add(powerupEffect);
                Destroy(gameObject);
            }
        }

        private IEnumerator RevertPowerup(GameObject player)
        {
            yield return new WaitForSeconds(powerupTime);
        
            powerupEffect.Revert(player);
        
            Destroy(gameObject);
        }
    }
}
