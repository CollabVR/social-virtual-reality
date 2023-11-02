using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;


public class AvatarItem : MonoBehaviour
{
    public string bodySkinName;
    private Button button;
    private Image image;

    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        button.onClick.AddListener(OnAvatarSelection);
        SetBodyItemIndicator();
    }

    void OnAvatarSelection()
    {
        var audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.itemSelected);
        SetBodySkin(bodySkinName);
    }

    void SetBodySkin(string bodySkinName)
    {
        var player = GameObject.Find("Player");
        var avatarManager = player.GetComponent<AvatarManager>();
        avatarManager.ChangeBodyMaterial(bodySkinName, player.GetPhotonView().ViewID);
    }

    void SetBodyItemIndicator()
    {
        var bodyTexture = PlayerPrefs.GetString(Constants.BODY_TEXTURE, "body-1-red");
        if (bodyTexture.Equals(bodySkinName))
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
        SetBodyItemIndicator();
    }
}
