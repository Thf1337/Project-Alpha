using System;
using UnityEngine;

namespace General.Interfaces
{
    public interface IHitBox
    {
        event Action<RaycastHit2D[]> OnDetected;
    }
}