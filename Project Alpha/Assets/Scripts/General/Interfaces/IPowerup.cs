using Powerup;
using Powerup.Effects;
using UnityEngine;

namespace General.Interfaces
{
    public interface IPowerup
    {
        public void Apply(GameObject target);
        
        public void Revert(GameObject target);
    }
}