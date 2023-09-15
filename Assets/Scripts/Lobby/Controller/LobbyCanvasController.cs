using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Unity.VisualScripting;
using Photon.Voice.PUN.UtilityScripts;
using UnityEngine.UI;
using TMPro;

public class LobbyCanvasController : MonoBehaviourPunCallbacks
{
    public GameObject loadingPanel;
    public GameObject roomsPanel;
    public GameObject authPanel;

    ActivityService activityService = new ActivityService();

    void Start()
    {
        ShowLoadingPanel();
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conected to master:");
        ShowAuthPanel();

        Debug.Log("Joining to Lobby:");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("On disconnected Cause: " + cause);
        var text = loadingPanel.GetComponentInChildren<TMP_Text>();
        text.text = "Ha ocurrido un error: " + cause;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // var roomListPanel = roomsPanel.GetComponentInChildren<RoomListView>();
        // roomListPanel.updateRoomList(roomList);
    }

    public void ShowLoadingPanel()
    {
        loadingPanel.SetActive(true);
        authPanel.SetActive(false);
        roomsPanel.SetActive(false);
    }

    public void ShowAuthPanel()
    {
        authPanel.SetActive(true);
        loadingPanel.SetActive(false);
        roomsPanel.SetActive(false);
    }

    public void ShowRoomsPanel()
    {
        roomsPanel.SetActive(true);
        loadingPanel.SetActive(false);
        authPanel.SetActive(false);
    }


}
