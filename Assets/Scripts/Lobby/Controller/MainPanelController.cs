using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject roomListPanel;
    public GameObject avatarListPanel;
    public GameObject settingsPanel;

    void Start()
    {

        ShowRoomsPanel();
    }

    public void ShowRoomsPanel()
    {
        roomListPanel.SetActive(true);
        avatarListPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowAvatarsPanel()
    {
        avatarListPanel.SetActive(true);
        roomListPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
        roomListPanel.SetActive(false);
        avatarListPanel.SetActive(false);
    }
}
