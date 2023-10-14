using ExitGames.Client.Photon;
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


    AudioSource audioSource;
    public Slider volumeSlider;


    void Start()
    {
        leaveRoom.onClick.AddListener(LeaveRoom);
        exitGame.onClick.AddListener(ExitGame);
        toggleMuted.onValueChanged.AddListener(delegate { MutePlayer(); });

        VoiceManager = GameObject.Find("VoiceManager");
        recorder = VoiceManager.GetComponent<Recorder>();
        audioSource = GetComponentInParent<AudioSource>();
        audioSource.volume = 0f;

        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });
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

    void SetVolume()
    {   
        audioSource.volume = volumeSlider.value;
    }

}
