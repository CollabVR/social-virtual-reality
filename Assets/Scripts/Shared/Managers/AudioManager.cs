using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    // add space en unity ui
    [Space(10)]
    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip buttonSelected;
    public AudioClip itemSelected;
    public AudioClip checkToggle;
    public AudioClip sliderChange;
    public AudioClip openOrCloseMenu;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }

    }

    void Start()
    {
        InitVolume();
        PlayBGMusic(backgroundMusic);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (Photon.Pun.PhotonNetwork.InLobby || Photon.Pun.PhotonNetwork.InRoom))
        {
            PlaySFX(openOrCloseMenu);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayBGMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }


    public void InitVolume()
    {
        musicSource.volume = PlayerPrefs.GetFloat(Constants.MUSIC_VOLUME, 1f);
        sfxSource.volume = PlayerPrefs.GetFloat(Constants.SFX_VOLUME, 1f);
    }
}
