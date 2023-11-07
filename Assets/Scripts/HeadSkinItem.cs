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
        var audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.itemSelected);
        SetHeadSkin(headSkinName);
    }

    void SetHeadSkin(string headSkinName)
    {
        PlayerPrefs.SetString(Constants.HEAD_TEXTURE, headSkinName);

        if (PhotonNetwork.InLobby)
        {
            var player = GameObject.Find("Player");
            var avatarManager = player.GetComponent<AvatarManager>();

            Debug.Log("Changing material in local");
            avatarManager.ChangeHeadMaterial(headSkinName);
        }
        else
        {
            Debug.Log("Changing material in remote");
            var avatarManager = GetComponentInParent<AvatarManager>();
            avatarManager.photonView.RPC("ChangeHeadMaterial", RpcTarget.AllBufferedViaServer, headSkinName);
        }

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
