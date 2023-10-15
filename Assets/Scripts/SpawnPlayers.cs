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
    }

}
