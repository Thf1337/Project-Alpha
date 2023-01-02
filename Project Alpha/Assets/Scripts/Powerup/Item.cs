using UnityEngine;

namespace Powerup
{
    public abstract class Item : MonoBehaviour
    {
        [field: SerializeField] public int ItemID { get; private set; }
        
        [field: SerializeField] public float Weight { get; private set; }

        public GameObject Instantiate()
        {
            return new GameObject();
        }
    }
}