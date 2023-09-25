using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

// Deprecated
public class CreateJoinRoomView : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomName;

    public void CreateRoom() 
    {
        // Cuando se crea una sala, tambien se une a la misma automaticamente
        // Debug.Log("Creating Room with name: -" + roomName.text + "-");
        // PhotonNetwork.CreateRoom(roomName.text);
    }

    public void JoinRoom()
    {
        // Debug.Log("Joining to Room with Room Name: -" +roomName.text + "-");
        // Debug.Log("Joining to Room with username: -" + PhotonNetwork.NickName + "-");

        // PhotonNetwork.JoinRoom(roomName.text);
    }

    public override void OnCreatedRoom() {
        // Debug.Log("Create Room With name: -" + roomName.text + "-");
    }

    public override void OnCreateRoomFailed(short code, string message) 
    {
        // Debug.Log("Room creation failed with code: " + code.ToString() + " - " + message);
    }

}
