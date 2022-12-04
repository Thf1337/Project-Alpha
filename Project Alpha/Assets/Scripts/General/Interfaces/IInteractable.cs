using UnityEngine;

namespace General.Interfaces
{
    public interface IInteractable
    {
        public bool HasInteracted();
        
        public void Interact(GameObject source);
    }
}
