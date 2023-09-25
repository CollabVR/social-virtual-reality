using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// RoomListing
public class RoomItem : MonoBehaviour
{
    [SerializeField]

    private TMP_Text _text;
    public RoomInfo RoomInfo { get; private set; }
    public Activity Activity { get; private set; }


    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name + " ";
    }

    public void OnClickButton()
    {
        // for: OnRoomListUpdate
        // Debug.Log("Selected Room: " + RoomInfo.Name);
        // PhotonNetwork.JoinRoom(RoomInfo.Name);

        SetControllerActivity(Activity);
        PhotonNetwork.JoinOrCreateRoom(Activity.name, new RoomOptions
        {
            MaxPlayers = Activity.maxParticipants
        }, TypedLobby.Default);
    }

    public void SetActivityInfo(Activity activity)
    {
        Activity = activity;
        _text.text = activity.name;
    }

    void SetControllerActivity(Activity activity)
    {
        var lobbyCanvasController = GameObject.Find("LobbyCanvasController").GetComponent<LobbyCanvasController>();
        lobbyCanvasController.selectedActivity = activity;
    }

}
