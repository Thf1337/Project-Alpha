﻿using System.Collections.Generic;
using System.Linq;
using Combat.Projectiles;

namespace Combat.Weapons.Modifiers
{
    public class ProjectileModifiers : ProjectileComponent
    {
        private List<AttackModifier> modifiers { get; set; } = new List<AttackModifier>();

        public bool TryGetModifier<T>(out T comp) where T : AttackModifier
        {
            comp = (T) modifiers.FirstOrDefault(item => item.GetType() == typeof(T));

            return comp != null;
        }

        public void SetModifiers(List<AttackModifier> mods)
        {
            modifiers = mods;
        }
    }
}