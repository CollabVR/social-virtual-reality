using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    public GameObject localXROriginCamera;

    public GameObject playerHead;
    public GameObject playerBody;


    void Awake()
    {
        if (photonView.IsMine || SceneManagerHelper.ActiveSceneBuildIndex == 0)
        {
            localXROriginCamera.SetActive(true);

            SetLayerRecursively(playerHead, 8);
            SetLayerRecursively(playerBody, 9);
        }
        else
        {
            localXROriginCamera.SetActive(false);

            SetLayerRecursively(playerHead, 0);
            SetLayerRecursively(playerBody, 0);
        }
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
