using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;

public class HeadSkinItem : MonoBehaviour
{
    public string headSkinName;
    private Button button;
    private Image image;

    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        button.onClick.AddListener(OnAvatarSelection);
        SetHeadItemIndicator();
    }

    void OnAvatarSelection()
    {
        SetHeadSkin(headSkinName);
    }

    void SetHeadSkin(string headSkinName)
    {
        var player = GameObject.Find("Player");
        var avatarManager = player.GetComponent<AvatarManager>();
        avatarManager.ChangeHeadMaterial(headSkinName, player.GetPhotonView().ViewID);
    }

    void SetHeadItemIndicator()
    {
        var headTexture = PlayerPrefs.GetString(Constants.HEAD_TEXTURE, "head-1-black");
        if (headTexture.Equals(headSkinName))
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
        SetHeadItemIndicator();
    }

}
