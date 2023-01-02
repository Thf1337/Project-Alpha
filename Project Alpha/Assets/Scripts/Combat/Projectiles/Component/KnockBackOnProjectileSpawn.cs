using Combat.Player;
using Controls;
using General.Interfaces;
using UnityEngine;

namespace Combat.Projectiles.Component
{
    public class KnockBackOnProjectileSpawn : ProjectileComponent<ProjectileSpawnKnockBackData>
    {
        public override void SetReferences()
        {
            base.SetReferences();
            
            Data = Projectile.Data.GetComponentData<ProjectileSpawnKnockBackData>();

            OnEnable();
        }

        private void SetDirection()
        {
            if (!Projectile.SpawningEntity)
            {
                return;
            }
            
            Data.KnockBackData.direction = -1 * Projectile.FacingDirection;
            PlayerMovement combat = Projectile.SpawningEntity.GetComponent<PlayerMovement>();

            combat.KnockBack(Data.KnockBackData);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            Projectile.OnInit += SetDirection;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            Projectile.OnInit -= SetDirection;
        }
    }

    public class ProjectileSpawnKnockBackData : ProjectileComponentData
    {
        public KnockBackData KnockBackData;

        public ProjectileSpawnKnockBackData()
        {
            ComponentDependencies.Add(typeof(KnockBackOnProjectileSpawn));
        }
    }
}
