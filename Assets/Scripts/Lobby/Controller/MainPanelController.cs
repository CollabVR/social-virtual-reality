using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject roomListPanel;
    public GameObject avatarListPanel;

    void Start()
    {

        ShowRoomsPanel();
    }

    public void ShowRoomsPanel()
    {
        roomListPanel.SetActive(true);
        avatarListPanel.SetActive(false);
    }

    public void ShowAvatarsPanel()
    {
        roomListPanel.SetActive(false);
        avatarListPanel.SetActive(true);
    }
}
