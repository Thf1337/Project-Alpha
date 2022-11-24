using UI;
using UnityEngine;

namespace Combat.Player
{
    public class PlayerMana : MonoBehaviour
    {
        public float mana;
        public float baseMaxMana = 50f;
        public float maxManaMultiplier = 0f;
    
        [SerializeField] private ManaBar manaBar;
    
        private float _maxMana;
    
        private void Start()
        {
            _maxMana = CalculateMaxMana();
            mana = _maxMana;
        
            manaBar.SetMaxMana(_maxMana);
            manaBar.SetMana(mana);
        }

        private float CalculateMaxMana()
        {
            return baseMaxMana + baseMaxMana * maxManaMultiplier;
        }

        public float GetMaxMana()
        {
            return _maxMana;
        }
    
        public void Restore(float restore)
        {
            mana += restore;
            manaBar.SetMana(mana);
        }

        public void Spend(float spent)
        {
            mana -= spent;
            manaBar.SetMana(mana);
        }

        public void AddMaxHealth(float addMaxMana, float addMaxManaMultiplier, bool restoreMana) {
            baseMaxMana += addMaxMana;
            maxManaMultiplier += addMaxManaMultiplier;
            float difference = CalculateMaxMana() - _maxMana;
            _maxMana += difference;
        
            manaBar.SetMaxMana(_maxMana);

            if (restoreMana)
            {
                Restore(difference);
            }
        }

        public void RemoveMaxHealth(float removeMaxMana, float removeMaxManaMultiplier) {
            baseMaxMana -= removeMaxMana;
            maxManaMultiplier -= removeMaxManaMultiplier;
            _maxMana = CalculateMaxMana();

            manaBar.SetMaxMana(_maxMana);

            if (mana > _maxMana)
            {
                mana = _maxMana;
                manaBar.SetMana(mana);
            }
        }
    }
}