using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.XR;

public class PlayerTransform : MonoBehaviour
{
    //Avatar Transforms
    [Header("Player Model")]
    public Transform playerTransform;
    public Transform playerHead;
    public Transform playerBody;
    public Transform playerHandRight;
    public Transform playerHandLeft;
    
    [Space]
    [Header("XR Origin")]
    public Transform XROrigin;
    public Transform XRHeadCamera;
    public Transform XRHandRight;
    public Transform XRHandLeft;

    [Space]
    [Header("Offset")]
    public Vector3 headPositionOffset;

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine || SceneManagerHelper.ActiveSceneBuildIndex == 0)
        {
            playerTransform.position = Vector3.Lerp(playerTransform.position, XRHeadCamera.position + headPositionOffset, 0.5f);
         
            // Head
            playerHead.rotation = Quaternion.Lerp(playerHead.rotation, XRHeadCamera.rotation, 0.5f);
            
            // Body
            playerBody.rotation = Quaternion.Lerp(playerBody.rotation, Quaternion.Euler(new Vector3(0, playerHead.rotation.eulerAngles.y, 0)), 0.05f);
            
            // Hand Position
            playerHandLeft.position  = Vector3.Lerp(playerHandLeft.position, XRHandLeft.position, 1f);
            playerHandRight.position = Vector3.Lerp(playerHandRight.position, XRHandRight.position, 1f);

            // Hand Rotation
            playerHandLeft.rotation  = Quaternion.Lerp(playerHandLeft.rotation, XRHandLeft.rotation, 1f);
            playerHandRight.rotation = Quaternion.Lerp(playerHandRight.rotation, XRHandRight.rotation, 0.5f);
        }

    }
}
