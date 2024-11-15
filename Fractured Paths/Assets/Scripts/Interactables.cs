// Used Kabungus tutorial on YouTube: https://www.youtube.com/watch?v=b7Yf6BFx6js
// Names: Braeden Watkins and Chris Ramos

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

    public void Interact(){
        onInteraction.Invoke();
    }

    public void DisableOutline(){
        outline.enabled = false;
    }

    public void EnableOutline(){
        outline.enabled = true;
    }
}
