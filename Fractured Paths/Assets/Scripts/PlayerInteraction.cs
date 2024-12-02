// Used Kabungus tutorial on YouTube: https://www.youtube.com/watch?v=b7Yf6BFx6js
// Names: Braeden Watkins and Chris Ramos
// Program: Used to make the buttons interactable/show outline when buttons being hovered over

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactables currentInteractable;

    public Camera newcamera;

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(newcamera.transform.position, newcamera.transform.forward);
        
        // if collides with anything within player reach
        if( Physics.Raycast(ray, out hit, playerReach))
        {
            if( hit.collider.tag == "Interactable" ) // if looking at an interactable object
            {
                Interactables newInteractable = hit.collider.GetComponent<Interactables>();
                
                // If there is a currentInteractable and it is not the new one
                if( currentInteractable && newInteractable != currentInteractable )
                {
                    currentInteractable.DisableOutline();
                }

                if( newInteractable.enabled )
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else // if new interactable is not enabled
                {
                    DisableCurrentInteractable();
                }
            }
            else // If not an interactable
            {
                DisableCurrentInteractable();
            }
        }
        else // if nothing in reach
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactables newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
    }

    void DisableCurrentInteractable()
    {
        if( currentInteractable )
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
