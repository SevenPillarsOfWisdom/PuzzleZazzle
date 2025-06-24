using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionPoint : MonoBehaviour
{
    [SerializeField] private string _interactionDescription;
    [SerializeField] private string _interactionTag = "Player";


    public string InteractionDescription => $"Press [F] to {_interactionDescription}";


    public UnityEvent OnInteract;

    public event Action<string> OnPlayerEnter;
    public event Action OnPlayerExit;

    private CharacterMovement _interactor;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(_interactionTag))
        {
            OnPlayerEnter?.Invoke(InteractionDescription);

            if(other.gameObject.TryGetComponent(out _interactor))
            {
                _interactor.SetInteractionPoint(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_interactionTag))
        {
            OnPlayerExit?.Invoke();
            if(_interactor)
            {
                _interactor.SetInteractionPoint(null);
            }
        }
    }

    public void Interact()
    {
        OnInteract.Invoke();
    }

}
