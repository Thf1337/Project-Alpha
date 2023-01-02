using UnityEngine;

namespace General.Interfaces
{
    public interface IProjectileCollisionEffect
    {
        public bool CheckHit(RaycastHit2D hit);
    }
}