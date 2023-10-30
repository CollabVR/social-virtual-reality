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
    public Slider cameraSensitivity;

    VoiceManager voiceManager;
    Recorder recorder;


    AudioSource audioSource;
    public Slider volumeSlider;

    PlayerMNKController playerMNKController;


    void Start()
    {
        playerMNKController = GetComponentInParent<PlayerMNKController>();
        voiceManager = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
        recorder = GameObject.Find("VoiceManager").GetComponent<Recorder>();

        leaveRoom.onClick.AddListener(LeaveRoom);
        exitGame.onClick.AddListener(ExitGame);

        toggleMuted.onValueChanged.AddListener(delegate { MutePlayer(); });

        volumeSlider.value = PlayerPrefs.GetFloat(Constants.MICROPHONE_VOLUME, 1); // 0 - 2.5
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });

        cameraSensitivity.value = PlayerPrefs.GetFloat(Constants.CAMERA_SENSITIVITY, 100);
        cameraSensitivity.onValueChanged.AddListener(delegate { SetCameraSensitivity(); });
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
        SceneManager.LoadSceneAsync("Lobby");
    }

    void MutePlayer()
    {
        voiceManager.MutePlayer();
    }

    void SetVolume()
    {
        PlayerPrefs.SetFloat(Constants.MICROPHONE_VOLUME, volumeSlider.value);
        voiceManager.SetMicrophoneVolume(volumeSlider.value);
    }

    void SetCameraSensitivity()
    {
        float value = cameraSensitivity.value;

        if (!playerMNKController.usingVR)
        {
            playerMNKController.mouseSensitivity = value;
        }

        PlayerPrefs.SetFloat(Constants.CAMERA_SENSITIVITY, value);
    }

    void ExitGame()
    {
        MetricsManager.Instance.SendActivityActionsToServer(
           action: "Left",
           playerCount: PhotonNetwork.CurrentRoom.PlayerCount - 1);

        MetricsManager.Instance.SendUserActionsToServer();
        MetricsManager.Instance.userTimeSpeaking = 0;

        PlayerPrefs.DeleteKey(Constants.IS_LOGGED);
        PlayerPrefs.DeleteKey(Constants.USER);

        Application.Quit();
    }
}
