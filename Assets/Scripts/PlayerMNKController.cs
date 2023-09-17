using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.XR;

public class PlayerMNKController : MonoBehaviour
{
    public GameObject XROrigin;
    public Transform mainCamera;
    public Transform playerModel;

    public float moveSpeed = 1;
    public float mouseSensitivity = 100f;

    // Networking
    PhotonView view;

    CharacterController characterController;
    float xRotation = 0f;
    XROrigin _xrOrigin;
    public bool usingVR;

    public bool canMove = true;

    void Start()
    {
        characterController = XROrigin.GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
        _xrOrigin = XROrigin.GetComponent<XROrigin>();
    }

    void Update()
    {

        usingVR = _xrOrigin.CurrentTrackingOriginMode != TrackingOriginModeFlags.Unknown;

        if (view.IsMine && !usingVR && canMove)
        {
            CameraRotation();
            MovePlayer();
            MoveJump();
        }
    }

    void MovePlayer() 
    {
        float horizontalMove = Input.GetAxis("Horizontal"); // x
        float verticalMove = Input.GetAxis("Vertical"); // z

        // Movement in local coordinates
        Vector3 move = XROrigin.transform.right * horizontalMove + XROrigin.transform.forward * verticalMove; 
        characterController.Move(move * moveSpeed * Time.deltaTime); 
    }

    void MoveJump() 
    {
        if (Input.GetButtonDown("Jump"))
        {
           // https://youtu.be/_QajrabyTJc   
        }
    }

    void CameraRotation() 
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        XROrigin.transform.Rotate(Vector3.up * mouseX);
    }
}
