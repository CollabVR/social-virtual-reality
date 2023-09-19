using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Voice.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuOptionsInGame : MonoBehaviour
{
    public Button leaveRoom;
    public Button exitGame;
    public Toggle toggleMuted;

    
    GameObject VoiceManager;
    Recorder recorder;


    void Start()
    {
        leaveRoom.onClick.AddListener(LeaveRoom);
        exitGame.onClick.AddListener(ExitGame);
        toggleMuted.onValueChanged.AddListener(delegate { MutePlayer(); });
        
        VoiceManager = GameObject.Find("VoiceManager");
        recorder = VoiceManager.GetComponent<Recorder>();
    }

    void Update()
    {
        
    }

    void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }

    void MutePlayer()
    {
        Debug.Log("MutePlayer");
        recorder.RecordingEnabled = !recorder.RecordingEnabled;
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
