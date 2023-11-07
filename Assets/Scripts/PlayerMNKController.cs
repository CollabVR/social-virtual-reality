using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMNKController : MonoBehaviour
{
    public GameObject XROrigin;
    public Transform mainCamera;

    public float moveSpeed = 1;
    public float speedMultiplier = 2f;
    public float fovSpeedOffset = 60f;
    public float jumpHeight = 1;
    public float mouseSensitivity = 100f;

    // Networking
    PhotonView view;
    XROrigin _xrOrigin;
    CharacterController characterController;
    ActionBasedContinuousMoveProvider _continuosMoveProvider;

    float xRotation = 0f;
    public bool usingVR;
    public bool canMove = true;

    public float _groundDistance;
    public bool _isGrounded = false;
    public Vector3 _groundDistanceOffset;
    Vector3 velocity;
    public float _gravity = -9.81f;


    void Start()
    {
        characterController = XROrigin.GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
        _xrOrigin = XROrigin.GetComponent<XROrigin>();
        _continuosMoveProvider = XROrigin.GetComponent<ActionBasedContinuousMoveProvider>();


    }

    void Update()
    {
        usingVR = _xrOrigin.CurrentTrackingOriginMode != TrackingOriginModeFlags.Unknown;
        _continuosMoveProvider.enabled = usingVR;

        if (!canMove) return;

        if ((view.IsMine && !usingVR) || SceneManagerHelper.ActiveSceneBuildIndex == 0)
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

        // button shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(move * moveSpeed * speedMultiplier * Time.deltaTime);
            if (Camera.main.fieldOfView < 70f) {
                Camera.main.fieldOfView += fovSpeedOffset * Time.deltaTime;
            }
        } else 
        {
            characterController.Move(move * moveSpeed * Time.deltaTime);
            if (Camera.main.fieldOfView > 60f) {
                Camera.main.fieldOfView -= fovSpeedOffset * Time.deltaTime;
            }
        }
        
    }

    void MoveJump()
    {
        Ray ray = new Ray(XROrigin.transform.position + _groundDistanceOffset, Vector3.down);

        Debug.DrawRay(ray.origin, ray.direction * _groundDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, _groundDistance))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }

        velocity.y += _gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

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
