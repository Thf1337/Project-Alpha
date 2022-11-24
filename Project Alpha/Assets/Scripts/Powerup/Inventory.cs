using System.Collections.Generic;
using Powerup.Effects;
using UnityEngine;

namespace Powerup
{
    public class Inventory : MonoBehaviour
    {
        public List<PowerupEffect> permanentPowerups;

        private void Start()
        {
            permanentPowerups = new List<PowerupEffect>();
        }

        private void RevertPowerup(int powerupID)
        {
            // TODO: revert powerups via the powerupID
        }
    }
}
