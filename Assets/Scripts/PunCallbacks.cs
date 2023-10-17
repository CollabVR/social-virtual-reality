using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PunCallbacks : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // user enter to room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player enter to room: " + newPlayer.NickName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player joined to room (BYGERMAN): " + PhotonNetwork.CurrentRoom.Name);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // base.OnJoinedRoom();
        // Debug.Log("Player joined to room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Player left the room");
    }


    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
    }

    public Dictionary<int, Player> PlayersInRoom()
    {
        return PhotonNetwork.CurrentRoom.Players;
    }

}
