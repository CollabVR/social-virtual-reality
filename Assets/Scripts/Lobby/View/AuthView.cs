using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthView : MonoBehaviour
{
    public LobbyCanvasController lobbyCanvasController;

    AuthService authService = new AuthService();

    [Header("Elements")]
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_Text message;

    public Button login;

    void Start()
    {
        login.onClick.AddListener(Login);
    }

    void Login()
    {
        Debug.Log("username: " + username.text);
        Debug.Log("password: " + password.text);

        StartCoroutine(authService.SignIn(username: username.text, password: password.text,
        success: (user) =>
        {
            authService.SaveUser(user);
            LoginSuccess();
        },
        error: (error) =>
        {
            message.text = error;
        }
        ));
    }

    void LoginSuccess()
    {
        lobbyCanvasController.ShowRoomsPanel();
    }

}
