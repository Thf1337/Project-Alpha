using System;
using UnityEngine;

namespace Combat.Weapons.Component.ComponentData.AttackData
{
    [Serializable]
    public class AttackDraw : AttackData
    {
        public AnimationCurve Curve;
        public float DrawTime;
    }
}