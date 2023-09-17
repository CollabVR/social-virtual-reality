using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public GameObject canvasMenu;

    private PlayerMNKController playerMNKController;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        playerMNKController = GetComponent<PlayerMNKController>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowCanvasMenu(!canvasMenu.activeSelf);
        }

        // if (playerMNKController.usingVR) {

        //     return;
        // }


    }

    void ShowCanvasMenu(bool active)
    {
        canvasMenu.SetActive(active);
        LockCursor(!active);
        playerMNKController.canMove = !playerMNKController.canMove;
    }

    void LockCursor(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
