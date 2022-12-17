using System;
using Combat.Projectiles;
using Combat.Weapons.Component.ComponentData;
using Combat.Weapons.Component.ComponentData.AttackData;
using General.Utilities;
using UnityEngine;

namespace Combat.Weapons.Component
{
    public class RangedWeaponAttack : WeaponComponent<AttackRangedData, AttackRanged>
    {
        public event Action<GameObject> OnProjectileSpawned;

        public event Func<int, int> OnSetNumberOfProjectiles;
        public event Func<Vector2, Vector2[]> OnSetProjectileDirection;

        private Vector2 offset;
        private Vector2 direction;
        
        private Transform projectileContainer;

        private int numberToSpawn = 1;
        
        private void SpawnProjectiles()
        {
            foreach (var point in CurrentAttackData.AttackData)
            {
                var spawnAmount = OnSetNumberOfProjectiles?.Invoke(numberToSpawn) ?? numberToSpawn;

                var position = transform.position;
                offset.Set(
                    position.x + point.offset.x * Movement.facingDirection,
                    position.y + point.offset.y
                );

                direction.Set(point.direction.x * Movement.facingDirection, point.direction.y);

                var directions = OnSetProjectileDirection?.Invoke(direction) ?? new Vector2[] { direction };

                for (var i = 0; (i < spawnAmount) || (i < directions.Length); i++)
                {
                    var projectile = Instantiate(
                        point.projectileData.ProjectilePrefab,
                        offset,
                        Quaternion.Euler(0f, 0f, VectorUtilities.AngleFromVector2(directions[i])),
                        projectileContainer);

                    var projectileScript = projectile.GetComponent<Projectile>();
                    projectileScript.CreateProjectile(point.projectileData);

                    OnProjectileSpawned?.Invoke(projectile);

                    projectileScript.Init(Weapon.BaseGameObject);
                }
            }
        }

        public override void SetReferences()
        {
            base.SetReferences();
            projectileContainer = GameObject.FindGameObjectWithTag("ProjectileContainer").transform;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            EventHandler.OnAttackAction += SpawnProjectiles;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventHandler.OnAttackAction -= SpawnProjectiles;
        }
    }
}