using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{

    void Start()
    {
        string selectedPrefabName = (string)PhotonNetwork.LocalPlayer.CustomProperties[Constants.AVATAR];
        if (selectedPrefabName == null) selectedPrefabName = "Player";

        PhotonNetwork.Instantiate(selectedPrefabName, transform.position, Quaternion.identity);
        // gameObject.GetComponent<AvatarManager>().SetBodyMaterial((string)PhotonNetwork.LocalPlayer.CustomProperties[Constants.BODY_TEXTURE], gameObject.GetPhotonView().ViewID);
        // Debug.Log("Instantiate player: " + gameObject.GetPhotonView().ViewID);
    }

}
