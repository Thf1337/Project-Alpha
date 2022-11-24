using UnityEngine;

namespace Powerup.Effects
{
    public abstract class PowerupEffect : ScriptableObject
    {
        public abstract void Apply(GameObject target);
        public abstract void Revert(GameObject target);
    }
}
