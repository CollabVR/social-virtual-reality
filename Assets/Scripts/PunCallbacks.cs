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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player enter to room: " + newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom: " + otherPlayer.NickName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player joined to room: " + PhotonNetwork.CurrentRoom.Name);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MetricsManager.Instance.SendActivityActionsToServer(
            action: "Join",
            playerCount: PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    public Dictionary<int, Player> PlayersInRoom()
    {
        return PhotonNetwork.CurrentRoom.Players;
    }

    void OnApplicationQuit()
    {

    }

}
