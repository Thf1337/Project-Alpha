using Combat.Player;
using UnityEngine;
using CombatScript = Combat.Combat;

namespace Powerup.Effects
{
    public class PotionDropBuff : PowerupComponent<PotionDropBuffData>
    {
        private CombatScript _combat;
        private Transform _targetTransform;
        private float _totalDamageDealt;


        public override void Apply(GameObject target)
        {
            _combat = target.GetComponent<CombatScript>();
            _targetTransform = target.GetComponent<Transform>();
            
            _combat.OnDamageDealt += DropPotion;
            
            base.Apply(target);
        }

        public override void Revert(GameObject target)
        {
            _combat.OnDamageDealt -= DropPotion;
        }

        private void DropPotion(float damage)
        {
            _totalDamageDealt += damage;

            if (_totalDamageDealt < Data.RequiredDamage) return;

            _totalDamageDealt -= Data.RequiredDamage;
            
            Data.Potion.SpawnPowerupAtTarget(_targetTransform);
        }
    }

    public class PotionDropBuffData : PowerupComponentData
    {
        public PowerupDataSO Potion;
        public float RequiredDamage;
        
        public PotionDropBuffData()
        {
            ComponentDependencies.Add(typeof(PotionDropBuff));
        }
    }
}