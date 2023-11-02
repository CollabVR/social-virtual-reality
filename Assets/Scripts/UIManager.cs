using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerMNKController MNKController;
    public Canvas canvas;
    RectTransform _canvasTransform;

    void Start()
    {
        _canvasTransform = canvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        SetupCanvas();

        if (MNKController.usingVR) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowOrHidenCanvas();
        }

    }

    void SetupCanvas()
    {
        if (!MNKController.usingVR)
        {
            MNKController.canMove = false;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.scaleFactor = 2.3f;
        }
        else
        {
            canvas.renderMode = RenderMode.WorldSpace;

            _canvasTransform.position = new Vector3(x: -0.01f, y: 1.8f, z: 4.0f);
            _canvasTransform.sizeDelta = new Vector2(x: 500, y: 300);
            _canvasTransform.rotation = new Quaternion(x: 0.0f, y: 0.0f, z: 0.0f, w: 1.0f);
            _canvasTransform.localScale = new Vector3(x: 0.01f, y: 0.01f, z: 0.01f);
        }
    }

    void ShowOrHidenCanvas()
    {
        if (canvas.enabled)
        {
            canvas.enabled = false;
            MNKController.canMove = true;
        }
        else
        {
            canvas.enabled = true;
            MNKController.canMove = false;
        }
    }
}
