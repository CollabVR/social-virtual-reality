using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class AvatarManager : MonoBehaviour
{
    public MeshRenderer headRenderer;
    public MeshRenderer bodyRenderer;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        var bodyMatName = PlayerPrefs.GetString(Constants.BODY_TEXTURE, "body-1-red");
        var headMatName = PlayerPrefs.GetString(Constants.HEAD_TEXTURE, "head-1-black");

        if (SceneManager.GetActiveScene().buildIndex == 0) // Lobby scene
        {
            // change material in local
            ChangeBodyMaterial(bodyMatName, gameObject.GetPhotonView().ViewID);
            ChangeHeadMaterial(headMatName, gameObject.GetPhotonView().ViewID);
        }
        else
        {
            // change material in remote
            photonView.RPC("ChangeBodyMaterial", RpcTarget.AllBuffered, bodyMatName, gameObject.GetPhotonView().ViewID);
            photonView.RPC("ChangeHeadMaterial", RpcTarget.AllBuffered, headMatName, gameObject.GetPhotonView().ViewID);
        }

    }

    [PunRPC]
    public void ChangeBodyMaterial(string materialName, int objectId)
    {
        GameObject avatar = PhotonView.Find(objectId).gameObject;
        Texture2D texture = Resources.Load<Texture2D>("Textures/bodies/" + materialName);
        var AvatarManager = avatar.GetComponent<AvatarManager>();
        AvatarManager.bodyRenderer.material.mainTexture = texture;

        PlayerPrefs.SetString(Constants.BODY_TEXTURE, materialName);
    }

    [PunRPC]
    public void ChangeHeadMaterial(string materialName, int objectId) {
        GameObject avatar = PhotonView.Find(objectId).gameObject;
        Texture2D texture = Resources.Load<Texture2D>("Textures/heads/" + materialName);
        var AvatarManager = avatar.GetComponent<AvatarManager>();
        AvatarManager.headRenderer.material.mainTexture = texture;

        PlayerPrefs.SetString(Constants.HEAD_TEXTURE, materialName);
    }

}
