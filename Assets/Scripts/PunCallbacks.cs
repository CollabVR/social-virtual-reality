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
        MetricsManager.Instance.SendActivityActionsToServer(
            action: "Left",
            playerCount: PhotonNetwork.CurrentRoom.PlayerCount - 1);

        MetricsManager.Instance.SendUserActionsToServer();
        MetricsManager.Instance.userTimeSpeaking = 0;

        PlayerPrefs.DeleteKey(Constants.IS_LOGGED);
        PlayerPrefs.DeleteKey(Constants.USER);
    }

}
