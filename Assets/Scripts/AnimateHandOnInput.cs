using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;


public class AnimateHandOnInput : MonoBehaviourPun
{
    public InputActionProperty triggerInput;
    public InputActionProperty gripInput;
    public Animator handAnimator;

    void Start()
    {
        
    }

    void Update()
    {
        if (photonView.IsMine) 
        {
            // XRI Hand Interaction/Activate Value -> float/double | XRI Hand Interaction/Activate -> boolean
            float triggerValue = triggerInput.action.ReadValue<float>();
            // XRI Hand Interaction/Select Value -> float/double | XRI Hand Interaction/Select -> boolean 
            float gripValue = gripInput.action.ReadValue<float>();

            handAnimator.SetFloat("Trigger", triggerValue);
            handAnimator.SetFloat("Grip", gripValue);
        }

    }
}
