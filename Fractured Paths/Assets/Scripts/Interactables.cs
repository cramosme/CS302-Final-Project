// Used Kabungus tutorial on YouTube: https://www.youtube.com/watch?v=b7Yf6BFx6js
// Names: Braeden Watkins and Chris Ramos
// Program: Creates an outline around the button being hovered over by the cursor
// if the player is close enough to the button. The outline is created using a downloaded
// outline pack off of the Unity Asset Store

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactables : MonoBehaviour
{
    Outline outline;
    public UnityEvent onInteraction;

    
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    // the following functions are used by PlayerInteraction.cs

    public void Interact(){
        onInteraction.Invoke();
    }

    // used to turn off the outline
    public void DisableOutline(){
        outline.enabled = false;
    }

    // used to turn on the outline
    public void EnableOutline(){
        outline.enabled = true;
    }
}
