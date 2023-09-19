using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMenu : MonoBehaviour
{
    public GameObject canvasMenu;

    private Canvas _canvas;
    private RectTransform _canvasTransform;

    private InputManager _inputManager;
    private PlayerMNKController _playerMNKController;
    private PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _playerMNKController = GetComponent<PlayerMNKController>();
        _canvas = canvasMenu.GetComponent<Canvas>();
        _canvasTransform = canvasMenu.GetComponent<RectTransform>();
        _inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        if (!_photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Escape) || _inputManager.RightPrimaryButtonDownPressed() || _inputManager.LeftPrimaryButtonDownPressed())
        {
            SetupCanvasTransformation();
            ShowCanvasMenu(!canvasMenu.activeSelf);
        }

    }
    
    void ShowCanvasMenu(bool active)
    {
        canvasMenu.SetActive(active);
        LockCursor(!active);
        _playerMNKController.canMove = !_playerMNKController.canMove;
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

    void SetupCanvasTransformation()
    {
        if (!_playerMNKController.usingVR)
        {
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.scaleFactor = 2;
        }
        else
        {
            _canvas.renderMode = RenderMode.WorldSpace;
        }
    }
}
