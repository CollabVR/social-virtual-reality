using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuOptionsInGame : MonoBehaviour
{
    public Button leaveRoom;
    public Button exitGame;

    void Start()
    {
        leaveRoom.onClick.AddListener(LeaveRoom);
        exitGame.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        
    }

    void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
