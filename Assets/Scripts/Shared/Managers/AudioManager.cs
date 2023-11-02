using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
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

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
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
}
