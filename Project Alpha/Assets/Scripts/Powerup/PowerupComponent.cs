using System.Collections;
using General.Interfaces;
using UnityEngine;

namespace Powerup
{
    public class PowerupComponent : MonoBehaviour
    {
        private Powerup powerup;

        protected Powerup Powerup
        {
            get => powerup ? powerup : (powerup = GetComponent<Powerup>());
            private set => powerup = value;
        }
        
        public virtual void SetReferences()
        {
            Powerup = GetComponent<Powerup>();
        }
        
        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }
    }

    public class PowerupComponent<T> : PowerupComponent, IPowerup where T : PowerupComponentData
    {
        protected T Data;

        public override void SetReferences()
        {
            base.SetReferences();

            Data = Powerup.Data.GetComponentData<T>();
        }

        public virtual void Apply(GameObject target)
        {
            if (Data.PowerupTime > 0f)
            {
                StartCoroutine(RevertPowerup(target));
            }
        }

        public virtual void Revert(GameObject target)
        {
            
        }

        private IEnumerator RevertPowerup(GameObject player)
        {
            yield return new WaitForSeconds(Data.PowerupTime);
        
            Revert(player);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Powerup.OnPowerupPickup += Apply;
        }

        protected override void OnDisable()
        {
            base.OnEnable();
            Powerup.OnPowerupPickup -= Apply;
        }
    }
}