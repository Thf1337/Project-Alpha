using System;
using System.Collections;
using System.Collections.Generic;
using General.Interfaces;
using TMPro;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputHelper;
    private IInteractable _lastInteractable;

    void Update()
    {
        if (_lastInteractable != null)
        {
            inputHelper.enabled = true;
        }
        else
        {
            inputHelper.enabled = false;
        }
        
        if (Input.GetButtonDown("Interact") && _lastInteractable != null)
        {
            _lastInteractable.Interact(gameObject);

            if (_lastInteractable == null || _lastInteractable.HasInteracted())
            {
                _lastInteractable = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            var interactable = collision.GetComponent<IInteractable>();

            if (interactable.HasInteracted())
            {
                return;
            }

            _lastInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _lastInteractable = null;
    }
}
