using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerVRController : MonoBehaviour
{
    InputManager inputManager;
    ActionBasedContinuousMoveProvider _continuosMoveProvider;
    PhotonView view;

    bool isRunning = false;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        _continuosMoveProvider = GetComponentInChildren<ActionBasedContinuousMoveProvider>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine) return;

        // Run();
    }

    void Run()
    {
        // TODO: verificar si es el boton correcto 

        // mantener para correr
        if (inputManager.RightSecondaryButtonPressed())
        {
            _continuosMoveProvider.moveSpeed = _continuosMoveProvider.moveSpeed * 2.5f;
        }
        else
        {
            _continuosMoveProvider.moveSpeed = _continuosMoveProvider.moveSpeed / 2.5f;
        }

        // correr cuando se presionan una vez (activar y desactivar)
        if (inputManager.RightSecondaryButtonDownPressed())
        {
            isRunning = !isRunning;
        }

        if (isRunning)
        {
            _continuosMoveProvider.moveSpeed = _continuosMoveProvider.moveSpeed * 2.5f;
        }
        else
        {
            _continuosMoveProvider.moveSpeed = _continuosMoveProvider.moveSpeed / 2.5f;
        }
    }
}
