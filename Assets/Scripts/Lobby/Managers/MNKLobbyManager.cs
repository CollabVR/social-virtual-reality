using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNKLobbyManager : MonoBehaviour
{
    bool mnkEnable = false;
    
    GameObject rightHand;
    GameObject leftHand;
    GameObject leftRay;
    GameObject rightRay;

    public Canvas canvas;

    void Start()
    {
        rightHand = GameObject.Find("Right Hand");
        leftHand  = GameObject.Find("Left Hand");
        leftRay   = GameObject.Find("Left Ray Interactor");
        rightRay  = GameObject.Find("Right Ray Interactor");      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            mnkEnable = !mnkEnable;

            if (mnkEnable) 
            {
                rightHand.SetActive(false);
                leftHand.SetActive(false);
                rightRay.SetActive(false);
                leftRay.SetActive(false);

                // Canvas.RenderMode = ScreenSpace.Overlay
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                // Ojo: Al hacer esto se setea el ReactTransform del Canvas.
                // Solucion: Alamcenar los valores del componente ReactTransform
            }
            else 
            {
                rightHand.SetActive(true);
                leftHand.SetActive(true);
                rightRay.SetActive(true);
                leftRay.SetActive(true);

                canvas.renderMode = RenderMode.WorldSpace;
                // Canvas.RenderMode = ScreenSpace.WorldSpace
                // Ojo: Al hacer esto se setea el ReactTransform del Canvas.
            } 
        }
    }
}
