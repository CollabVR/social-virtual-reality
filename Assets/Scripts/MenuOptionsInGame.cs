using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
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


    VoiceManager voiceManager;
    Recorder recorder;


    AudioSource audioSource;
    public Slider volumeSlider;


    void Start()
    {
        voiceManager = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
        recorder = GameObject.Find("VoiceManager").GetComponent<Recorder>();

        leaveRoom.onClick.AddListener(LeaveRoom);
        exitGame.onClick.AddListener(ExitGame);

        toggleMuted.onValueChanged.AddListener(delegate { MutePlayer(); });
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });
    }

    void Update()
    {

    }

    void LeaveRoom()
    {
        Debug.Log("Leaving room");
        MetricsManager.Instance.SendActivityActionsToServer(
            action: "Left",
            playerCount: PhotonNetwork.CurrentRoom.PlayerCount - 1);

        MetricsManager.Instance.SendUserActionsToServer();
        MetricsManager.Instance.userTimeSpeaking = 0;

        Destroy(voiceManager.gameObject);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }

    void MutePlayer()
    {
        voiceManager.MutePlayer();
    }

    void SetVolume()
    {
        voiceManager.SetMicrophoneVolume(volumeSlider.value);
    }

    void ExitGame()
    {
        MetricsManager.Instance.SendActivityActionsToServer(
           action: "Left",
           playerCount: PhotonNetwork.CurrentRoom.PlayerCount - 1);

        MetricsManager.Instance.SendUserActionsToServer();
        MetricsManager.Instance.userTimeSpeaking = 0;
        PlayerPrefs.DeleteAll();

        Application.Quit();
    }
}
