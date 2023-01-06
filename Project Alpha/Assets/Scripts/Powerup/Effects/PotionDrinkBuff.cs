using Combat.Player;
using Controls;
using UnityEngine;

namespace Powerup.Effects
{
    public class PotionDrinkBuff : PowerupComponent<PotionDrinkBuffData>
    {
        private Potion _potionScript;
        private Transform _targetTransform;
        private GameObject _target;
        
        public override void Apply(GameObject target)
        {
            _potionScript = target.GetComponent<Potion>();
            _targetTransform = target.GetComponent<Transform>();
            _target = target;
            
            _potionScript.OnPotionDrink += ApplyBuffs;
            
            base.Apply(target);
        }

        public override void Revert(GameObject target)
        {
            _potionScript.OnPotionDrink -= ApplyBuffs;
        }

        private void ApplyBuffs()
        {
            Data.PowerupData.SpawnPowerupAtTarget(_targetTransform, _target);
        }

    }

    public class PotionDrinkBuffData : PowerupComponentData
    {
        public PowerupDataSO PowerupData;
        
        public PotionDrinkBuffData()
        {
            ComponentDependencies.Add(typeof(PotionDrinkBuff));
        }
    }
}