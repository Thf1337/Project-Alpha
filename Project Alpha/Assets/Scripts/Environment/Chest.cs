using System;
using General.Interfaces;
using Powerup;
using UnityEngine;

namespace Environment
{
    public class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEngine.Object itemRef;
        [SerializeField] private LootTable lootTable;
        
        [SerializeField] private Sprite closedSprite;
        [SerializeField] private Sprite openSprite;
        [SerializeField] private Sprite emptySprite;

        private SpriteRenderer _spriteRenderer;

        private bool _isOpen;
        private bool _isEmpty;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public bool HasInteracted()
        {
            return _isEmpty;
        }

        public void Interact(GameObject source)
        {
            if (_isEmpty)
            {
                return;
            }
            
            if (_isOpen)
            {
                _isEmpty = true;
                _spriteRenderer.sprite = emptySprite;
            }
            else
            {
                _isOpen = true;
                _spriteRenderer.sprite = openSprite;
                
                var position = transform.position;
                var item = lootTable.GetRandomItem();
                var itemInstantiate = Instantiate(item);
                itemInstantiate.transform.position = new Vector2(position.x, position.y + 1);
            }
        }
    }
}
