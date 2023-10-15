using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isTrigger = false;

    public void Interact()
    {
        Debug.Log("Interactable Interact()");
    }

    public void Interact(Action<Interactable> action)
    {
        Debug.Log("Interactable Interact(Action action)");
        action(this);
    }

    void Update()
    {

    }
    
}
