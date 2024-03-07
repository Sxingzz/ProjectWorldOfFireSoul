using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private NPCInteractable currentInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPCInteractable npcinteractable))
                {
                    if (npcinteractable != currentInteractable)
                    {
                        if (currentInteractable != null)
                        {
                            currentInteractable.OnExitInteractRange();
                        }

                        currentInteractable = npcinteractable;
                        currentInteractable.OnEnterInteractRange();
                        currentInteractable.Interact();
                    }
                }
            }
        }

        if (currentInteractable != null)
        {
            currentInteractable.UpdateDialogue();
        }
    }
}



