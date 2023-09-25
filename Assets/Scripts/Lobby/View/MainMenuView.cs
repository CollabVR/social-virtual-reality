using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Button activitesButton;
    public Button avatarsButton;
    public Button exitButton;

    private MainPanelController mainPanelController;


    void Start()
    {
        mainPanelController = GetComponentInParent<MainPanelController>();

        activitesButton.onClick.AddListener(OnActivitiesButtonSelected);
        avatarsButton.onClick.AddListener(OnAvatarsButtonSelected);
        exitButton.onClick.AddListener(OnExitButtonSelected);
    }

    void OnActivitiesButtonSelected()
    {
        mainPanelController.ShowRoomsPanel();

    }

    void OnAvatarsButtonSelected()
    {
        mainPanelController.ShowAvatarsPanel();
    }

    void OnExitButtonSelected()
    {
        Application.Quit();
    }
}
