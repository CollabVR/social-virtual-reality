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
    public GameObject mainPanel;
    public GameObject authPanel;

    public Activity selectedActivity;

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

    public override void OnJoinedRoom()
    {
        Debug.Log("Joining to: " + selectedActivity.name);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PhotonNetwork.LoadLevel(selectedActivity.environmentId);
    }

    public override void OnJoinRoomFailed(short code, string message)
    {
        Debug.Log("OnJoinRoomFailed -> CODE: " + code);
        Debug.Log("OnJoinRoomFailed: Message -> " + message);
    }

    public void ShowLoadingPanel()
    {
        loadingPanel.SetActive(true);
        authPanel.SetActive(false);
        mainPanel.SetActive(false);
    }

    public void ShowAuthPanel()
    {
        authPanel.SetActive(true);
        loadingPanel.SetActive(false);
        mainPanel.SetActive(false);
    }

    public void ShowRoomsPanel()
    {
        mainPanel.SetActive(true);
        loadingPanel.SetActive(false);
        authPanel.SetActive(false);
    }



}
