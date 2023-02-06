using System;
using System.Collections;
using System.Collections.Generic;
using General.Interfaces;
using TMPro;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float interactRadius;
    public LayerMask interactableLayerMask;
    [SerializeField] private TextMeshProUGUI inputHelper;
    // private IInteractable _lastInteractable;

    void Update()
    {
        var hits = Physics2D.OverlapBoxAll(transform.position, 
            new (interactRadius, interactRadius), 0f, interactableLayerMask);
        
        if (hits.Length == 0)
        {
            inputHelper.enabled = false;
            return;
        }

        foreach (var hit in hits)
        {
            var interactable = hit.GetComponent<IInteractable>();

            if (interactable == null || interactable.HasInteracted())
            {
                continue;
            }
        
            inputHelper.enabled = true;
            if (Input.GetButtonDown("Interact"))
            {
                interactable.Interact(gameObject);
                return;
            }
        }
    }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Interactable"))
    //     {
    //         var interactable = collision.GetComponent<IInteractable>();
    //
    //         if (interactable.HasInteracted())
    //         {
    //             return;
    //         }
    //
    //         _lastInteractable = interactable;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     _lastInteractable = null;
    // }
}
