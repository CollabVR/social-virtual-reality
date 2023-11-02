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


    void Start()
    {
        mainPanelController = GetComponentInParent<MainPanelController>();

        activitesButton.onClick.AddListener(OnActivitiesButtonSelected);
        avatarsButton.onClick.AddListener(OnAvatarsButtonSelected);
        settingsButton.onClick.AddListener(OnSettingsButtonSelected);
        signoffButton.onClick.AddListener(OnSignOffButtonSelected);

    }

    void OnActivitiesButtonSelected()
    {
        mainPanelController.ShowRoomsPanel();

    }

    void OnAvatarsButtonSelected()
    {
        mainPanelController.ShowAvatarsPanel();
    }

    void OnSignOffButtonSelected()
    {
        LobbyCanvasController lobbyCanvasController = GameObject.Find("LobbyCanvasController").GetComponent<LobbyCanvasController>();
        PlayerPrefs.DeleteKey(Constants.IS_LOGGED);
        PlayerPrefs.DeleteKey(Constants.USER);

        lobbyCanvasController.ShowAuthPanel();
    }

    void OnSettingsButtonSelected()
    {
        mainPanelController.ShowSettingsPanel();
    }
}
