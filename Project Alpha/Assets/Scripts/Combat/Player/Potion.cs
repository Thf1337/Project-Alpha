using System;
using Powerup;
using Powerup.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.Player
{
    public class Potion : MonoBehaviour
    {
        public PowerupDataSO CurrentPotion { get; private set; }

        [SerializeField] private Image potionImage;
        [SerializeField] private Sprite emptySprite;

        public event Action OnPotionDrink;

        private void Awake()
        {
            potionImage.sprite = emptySprite;
        }

        public void PickupPotion(PowerupDataSO potion)
        {
            var previousPotion = CurrentPotion;
            
            CurrentPotion = potion;
            potionImage.sprite = CurrentPotion.PowerupSprite;

            if (!previousPotion) return;
            
            previousPotion.SpawnPowerupAtTarget(transform);
        }

        private void Update()
        {
            if (Input.GetButtonDown("DrinkPotion") && CurrentPotion)
            {
                CurrentPotion.SpawnPowerupAtTarget(transform, gameObject);
                CurrentPotion = null;
                potionImage.sprite = emptySprite;
                
                OnPotionDrink?.Invoke();
            }
        }
    }
}