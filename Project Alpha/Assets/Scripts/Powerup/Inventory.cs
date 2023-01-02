using System.Collections.Generic;
using Powerup.Effects;
using UnityEngine;

namespace Powerup
{
    public class Inventory : MonoBehaviour
    {
        public List<PowerupEffectSO> permanentPowerups;

        private void Start()
        {
            permanentPowerups = new List<PowerupEffectSO>();
        }

        private void RevertPowerup(int powerupID)
        {
            // TODO: revert powerups via the powerupID
        }
    }
}
