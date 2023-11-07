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
    [Header("Canvas")]
    public GameObject menuOptionCanvas;
    public GameObject avatarCanvas;

    [Space(8)]
    [Header("Buttons")]
    public Button changeAvatar;
    public Button leaveRoom;
    public Button exitGame;
    public Button backToMenu;

    [Space(8)]
    [Header("Toggle")]
    public Toggle toggleMuted;

    [Space(8)]
    [Header("camera sensitivity")]
    public Slider cameraSensitivity;

    [Space(8)]
    [Header("Music Volume")]
    public Slider musicVolume;

    [Space(8)]
    [Header("SFX Volume")]
    public Slider sfxVolume;


    VoiceManager voiceManager;
    Recorder recorder;

    AudioManager audioManager;

    AudioSource audioSource;

    [Space(8)]
    [Header("Microphone Volume")]
    public Slider volumeSlider;

    PlayerMNKController playerMNKController;


    void Start()
    {
        playerMNKController = GetComponentInParent<PlayerMNKController>();
        voiceManager = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
        recorder = GameObject.Find("VoiceManager").GetComponent<Recorder>();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        changeAvatar.onClick.AddListener(ChangeAvatar);
        leaveRoom.onClick.AddListener(LeaveRoom);
        exitGame.onClick.AddListener(ExitGame);
        backToMenu.onClick.AddListener(backToMenuFromAvatar);

        toggleMuted.onValueChanged.AddListener(delegate { MutePlayer(); });

        volumeSlider.value = PlayerPrefs.GetFloat(Constants.MICROPHONE_VOLUME, 1); // 0 - 2.5
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });

        cameraSensitivity.value = PlayerPrefs.GetFloat(Constants.CAMERA_SENSITIVITY, 100);
        cameraSensitivity.onValueChanged.AddListener(delegate { SetCameraSensitivity(); });

        // music volume
        musicVolume.value = PlayerPrefs.GetFloat(Constants.MUSIC_VOLUME, audioManager.musicSource.volume);
        musicVolume.onValueChanged.AddListener(delegate { SetMusicVolume(); });

        // sfx volume
        sfxVolume.value = PlayerPrefs.GetFloat(Constants.SFX_VOLUME, audioManager.sfxSource.volume);
        sfxVolume.onValueChanged.AddListener(delegate { SetSFXVolume(); });


    }

    void Update()
    {

    }

    void backToMenuFromAvatar()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);

        menuOptionCanvas.SetActive(true);
        avatarCanvas.SetActive(false);
    }

    void ChangeAvatar()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);

        menuOptionCanvas.SetActive(false);
        avatarCanvas.SetActive(true);


        Debug.Log("Changing avatar");
    }

    void LeaveRoom()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);

        Debug.Log("Leaving room");
        MetricsManager.Instance.SendActivityActionsToServer(
            action: "Left",
            playerCount: PhotonNetwork.CurrentRoom.PlayerCount - 1);

        MetricsManager.Instance.SendUserActionsToServer();
        MetricsManager.Instance.userTimeSpeaking = 0;
        
        DestroyPlayerObjectInNetwork();
        Destroy(voiceManager.gameObject);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }

    void MutePlayer()
    {
        audioManager.PlaySFX(audioManager.checkToggle);
        voiceManager.MutePlayer();
    }

    void SetVolume()
    {
        audioManager.PlaySFX(audioManager.sliderChange);

        PlayerPrefs.SetFloat(Constants.MICROPHONE_VOLUME, volumeSlider.value);
        voiceManager.SetMicrophoneVolume(volumeSlider.value);
    }

    void SetCameraSensitivity()
    {
        audioManager.PlaySFX(audioManager.sliderChange);

        float value = cameraSensitivity.value;

        if (!playerMNKController.usingVR)
        {
            playerMNKController.mouseSensitivity = value;
        }

        PlayerPrefs.SetFloat(Constants.CAMERA_SENSITIVITY, value);
    }

    void SetMusicVolume()
    {
        audioManager.PlaySFX(audioManager.sliderChange);

        float value = musicVolume.value;

        audioManager.musicSource.volume = value;
        PlayerPrefs.SetFloat(Constants.MUSIC_VOLUME, value);
    }

    void SetSFXVolume()
    {
        audioManager.PlaySFX(audioManager.sliderChange);

        float value = sfxVolume.value;

        audioManager.sfxSource.volume = value;
        PlayerPrefs.SetFloat(Constants.SFX_VOLUME, value);
    }

    void ExitGame()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);

        MetricsManager.Instance.SendActivityActionsToServer(
           action: "Left",
           playerCount: PhotonNetwork.CurrentRoom.PlayerCount - 1);

        MetricsManager.Instance.SendUserActionsToServer();
        MetricsManager.Instance.userTimeSpeaking = 0;

        DestroyPlayerObjectInNetwork();

        PlayerPrefs.DeleteKey(Constants.IS_LOGGED);
        PlayerPrefs.DeleteKey(Constants.USER);

        Application.Quit();
    }

    void DestroyPlayerObjectInNetwork()
    {
        var avatarManager = GetComponentInParent<AvatarManager>();
        PhotonNetwork.Destroy(avatarManager.gameObject);
    }
}
