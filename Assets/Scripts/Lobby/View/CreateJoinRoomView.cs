using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateJoinRoomView : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomName;

    public void CreateRoom() 
    {
        // Cuando se crea una sala, tambien se une a la misma automaticamente
        Debug.Log("Creating Room with name: -" + roomName.text + "-");
        PhotonNetwork.CreateRoom(roomName.text);

        // using JoinOrCreateRoom
        // RoomOptions roomOptions = new RoomOptions
        // {
        //     MaxPlayers = 10
        // };
        // PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);
    }

    public void JoinRoom()
    {
        Debug.Log("Joining to Room with Room Name: -" +roomName.text + "-");
        Debug.Log("Joining to Room with username: -" + PhotonNetwork.NickName + "-");

        PhotonNetwork.JoinRoom(roomName.text);
    }

    public override void OnCreatedRoom() {
        Debug.Log("Create Room With name: -" + roomName.text + "-");
    }

    public override void OnCreateRoomFailed(short code, string message) 
    {
        Debug.Log("Room creation failed with code: " + code.ToString() + " - " + message);
    }

    public override void OnJoinedRoom() 
    {
        Debug.Log("Joined to Room: -" + roomName.text + "-");
        Debug.Log("Joined to Room with username: -" + PhotonNetwork.NickName + "-");
        PhotonNetwork.LoadLevel("Dev Scene");
    }

    public override void OnJoinRoomFailed(short code, string message) 
    {
        Debug.Log("OnJoinRoomFailed -> CODE: " + code);
        Debug.Log("OnJoinRoomFailed: Message -> " + message);
    }

}
