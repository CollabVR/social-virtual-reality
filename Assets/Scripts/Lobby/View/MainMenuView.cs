using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Button activitesButton;
    public Button avatarsButton;
    public Button settingsButton;
    public Button signoffButton;

    private MainPanelController mainPanelController;
    private AudioManager audioManager;


    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        mainPanelController = GetComponentInParent<MainPanelController>();

        activitesButton.onClick.AddListener(OnActivitiesButtonSelected);
        avatarsButton.onClick.AddListener(OnAvatarsButtonSelected);
        settingsButton.onClick.AddListener(OnSettingsButtonSelected);
        signoffButton.onClick.AddListener(OnSignOffButtonSelected);

    }

    void OnActivitiesButtonSelected()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);
        mainPanelController.ShowRoomsPanel();
    }

    void OnAvatarsButtonSelected()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);
        mainPanelController.ShowAvatarsPanel();
    }

    void OnSignOffButtonSelected()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);
        LobbyCanvasController lobbyCanvasController = GameObject.Find("LobbyCanvasController").GetComponent<LobbyCanvasController>();
        PlayerPrefs.DeleteKey(Constants.IS_LOGGED);
        PlayerPrefs.DeleteKey(Constants.USER);

        lobbyCanvasController.ShowAuthPanel();
    }

    void OnSettingsButtonSelected()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);
        mainPanelController.ShowSettingsPanel();
    }
}
