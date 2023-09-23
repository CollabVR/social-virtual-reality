using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthView : MonoBehaviour
{
    public LobbyCanvasController lobbyCanvasController;

    AuthService authService = new AuthService();

    [Header("Elements")]
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_Text message;

    public Button login;

    void Start()
    {
        login.onClick.AddListener(Login);
    }

    void Login()
    {
        StartCoroutine(authService.SignIn(username: email.text, password: password.text,
        success: (user) =>
        {
            authService.SaveUser(user);
            PhotonNetwork.NickName = user.fullName;
            lobbyCanvasController.ShowRoomsPanel();
        },
        error: (error) =>
        {
            if (Debug.isDebugBuild)
            {
                PhotonNetwork.NickName = email.text;
                Hashtable playerCustomProps = new Hashtable();
                playerCustomProps["avatar"] = email.text;;
                PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProps);

                lobbyCanvasController.ShowRoomsPanel();
            }
            message.text = error;
        }
        ));
    }

}
