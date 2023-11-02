using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// import for use TMPro text
using TMPro;

public class SettingsView : MonoBehaviour
{
    public Slider cameraSensitivity;
    public TMP_Text cameraSensitivityValue;

    PlayerMNKController playerMNKController;

    void Start()
    {
        playerMNKController = GameObject.Find("Player").GetComponent<PlayerMNKController>();

        cameraSensitivity.value = PlayerPrefs.GetFloat(Constants.CAMERA_SENSITIVITY, 100);
        cameraSensitivityValue.text = ((int)cameraSensitivity.value).ToString();
        cameraSensitivity.onValueChanged.AddListener(delegate { SetCameraSensitivity(); });

    }

    void SetCameraSensitivity()
    {
        float value = cameraSensitivity.value;
        cameraSensitivityValue.text = ((int)cameraSensitivity.value).ToString();

        if (!playerMNKController.usingVR)
        {
            playerMNKController.mouseSensitivity = value;
        }

        PlayerPrefs.SetFloat(Constants.CAMERA_SENSITIVITY, cameraSensitivity.value);
    }
}
