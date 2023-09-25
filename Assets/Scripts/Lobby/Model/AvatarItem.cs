using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;


public class AvatarItem : MonoBehaviour
{
    private TMP_Text avatarName;
    private Button button;
    private Image image;

    void Start()
    {
        button = GetComponent<Button>();
        avatarName = GetComponentInChildren<TMP_Text>();
        image = GetComponent<Image>();

        button.onClick.AddListener(OnAvatarSelection);

        SetPrefabAvatarName(PlayerPrefs.GetString(Constants.AVATAR, "Player")); // Default avatar name
    }

    void OnAvatarSelection()
    {
        SetPrefabAvatarName(avatarName.text);
        SavePrefabAvatarName();
    }

    void SetPrefabAvatarName(string avatarName)
    {
        Hashtable playerCustomProps = new Hashtable();
        playerCustomProps[Constants.AVATAR] = avatarName;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProps);
    }

    void SavePrefabAvatarName() 
    {
        PlayerPrefs.SetString(Constants.AVATAR, avatarName.text);
    }

    void SelectedAvatarIndicator()
    {
        if (avatarName.text == PhotonNetwork.LocalPlayer.CustomProperties[Constants.AVATAR].ToString())
        {
            button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            button.GetComponent<Image>().color = Color.gray;
        }
    }

    void OnGUI()
    {
        SelectedAvatarIndicator();
    }

}
