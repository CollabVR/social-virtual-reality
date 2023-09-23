using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    void Start()
    {

        // Debug.Log(photonView.Owner.NickName);
        // string avatar = photonView.Owner.NickName.Split(',')[1];
        // Debug.Log(avatar);

        // string avatar = photonView.Owner.CustomProperties["avatar"].ToString();

        // only needs the name of avatar:
        // PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);    

        string selectedPrefabName = (string)PhotonNetwork.LocalPlayer.CustomProperties["avatar"];
        Debug.Log(selectedPrefabName);
        PhotonNetwork.Instantiate(selectedPrefabName, transform.position, Quaternion.identity);

    }



}
