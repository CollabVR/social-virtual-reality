using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class AvatarManager : MonoBehaviour
{
    public MeshRenderer headRenderer;
    public MeshRenderer bodyRenderer;

    public PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        var bodyMatName = PlayerPrefs.GetString(Constants.BODY_TEXTURE, "body-1-red");
        var headMatName = PlayerPrefs.GetString(Constants.HEAD_TEXTURE, "head-1-black");

        if (SceneManager.GetActiveScene().buildIndex == 0) // Lobby scene
        {
            // change material in local
            Debug.Log("Changing material in local");
            ChangeBodyMaterial(bodyMatName);
            ChangeHeadMaterial(headMatName);
        }
        else
        {
            // change material in remote
            if (!photonView.IsMine) return;
            Debug.Log("Changing material in remote (Start) for " + gameObject.GetPhotonView().ViewID);

            photonView.RPC(nameof(ChangeBodyMaterial), RpcTarget.AllBuffered, bodyMatName);
            photonView.RPC(nameof(ChangeHeadMaterial), RpcTarget.AllBuffered, headMatName);
        }

    }

    [PunRPC]
    public void ChangeBodyMaterial(string materialName)
    {
        Debug.Log("Changing material in remote (RPC) for " + gameObject.GetPhotonView().ViewID);
        Texture2D texture = Resources.Load<Texture2D>("Textures/bodies/" + materialName);
        bodyRenderer.material.mainTexture = texture;

    }

    [PunRPC]
    public void ChangeHeadMaterial(string materialName)
    {
        Texture2D texture = Resources.Load<Texture2D>("Textures/heads/" + materialName);
        headRenderer.material.mainTexture = texture;

    }

}
