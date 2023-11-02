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
    private AudioManager audioManager;

    AuthService authService = new AuthService();

    [Header("Elements")]
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_Text message;

    public Button login;
    public Button exit;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        login.onClick.AddListener(Login);
        exit.onClick.AddListener(Exit);

        password.contentType = TMP_InputField.ContentType.Password;
    }

    void Login()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);

        StartCoroutine(authService.SignIn(username: email.text, password: password.text,
        success: (user) =>
        {
            authService.SaveUser(user);
            PhotonNetwork.NickName = user.fullName;
            MetricsManager.Instance.currentUser = user;
            lobbyCanvasController.ShowRoomsPanel();

            PlayerPrefs.SetInt(Constants.IS_LOGGED, 1);
        },
        error: (error) =>
        {
            if (Debug.isDebugBuild)
            {
                PhotonNetwork.NickName = email.text;
                lobbyCanvasController.ShowRoomsPanel();
            }
            message.text = error;
        }
        ));
    }

    void Exit()
    {
        audioManager.PlaySFX(audioManager.buttonSelected);
        Application.Quit();
    }
}
