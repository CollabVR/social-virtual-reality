using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    AudioSource sfxSource;

    // add space en unity ui
    [Space(10)]
    [Header("Audio Clips")] 
    public AudioSource backgroundMusic;
    public AudioClip buttonSelected;
    public AudioClip itemSelected;
    public AudioClip checkToggle;
    public AudioClip sliderChange;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // musicSource.clip = backgroundMusic.clip;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    } 
}
