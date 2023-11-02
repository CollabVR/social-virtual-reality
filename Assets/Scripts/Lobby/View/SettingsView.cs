using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// import for use TMPro text
using TMPro;

public class SettingsView : MonoBehaviour
{
    [Header("Camera Sensitivity")]
    public Slider cameraSensitivity;
    public TMP_Text cameraSensitivityValue;

    [Space(10)]
    [Header("Music Volume")]
    public Slider musicVolume;
    public TMP_Text musicVolumeValue;

    [Space(10)]
    [Header("SFX Volume")]
    public Slider sfxVolume;
    public TMP_Text sfxVolumeValue;


    PlayerMNKController playerMNKController;
    AudioManager audioManager;

    void Start()
    {
        playerMNKController = GameObject.Find("Player").GetComponent<PlayerMNKController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // camera sensitivity
        cameraSensitivity.value = PlayerPrefs.GetFloat(Constants.CAMERA_SENSITIVITY, 100);
        cameraSensitivityValue.text = ((int)cameraSensitivity.value).ToString();
        cameraSensitivity.onValueChanged.AddListener(delegate { SetCameraSensitivity(); });

        // music volume
        musicVolume.value = PlayerPrefs.GetFloat(Constants.MUSIC_VOLUME, audioManager.musicSource.volume);
        musicVolumeValue.text = (musicVolume.value * 100).ToString().Split('.')[0];
        musicVolume.onValueChanged.AddListener(delegate { SetMusicVolume(); });

        // sfx volume
        sfxVolume.value = PlayerPrefs.GetFloat(Constants.SFX_VOLUME, audioManager.sfxSource.volume);
        sfxVolumeValue.text = (sfxVolume.value * 100).ToString().Split('.')[0];
        sfxVolume.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }

    void SetCameraSensitivity()
    {
        audioManager.PlaySFX(audioManager.sliderChange);

        float value = cameraSensitivity.value;
        cameraSensitivityValue.text = ((int)cameraSensitivity.value).ToString();

        if (!playerMNKController.usingVR)
        {
            playerMNKController.mouseSensitivity = value;
        }

        PlayerPrefs.SetFloat(Constants.CAMERA_SENSITIVITY, cameraSensitivity.value);
    }

    void SetMusicVolume()
    {
        audioManager.PlaySFX(audioManager.sliderChange);

        float value = musicVolume.value;
        musicVolumeValue.text = (musicVolume.value * 100).ToString().Split('.')[0];

        audioManager.musicSource.volume = value;
        PlayerPrefs.SetFloat(Constants.MUSIC_VOLUME, musicVolume.value);
    }

    void SetSFXVolume()
    {
        audioManager.PlaySFX(audioManager.sliderChange);

        float value = sfxVolume.value;
        sfxVolumeValue.text = (sfxVolume.value * 100).ToString().Split('.')[0];

        audioManager.sfxSource.volume = value;
        PlayerPrefs.SetFloat(Constants.SFX_VOLUME, sfxVolume.value);
    }

}
