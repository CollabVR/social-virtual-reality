using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;
using System.Text;

public class AuthService
{
    public IEnumerator SignIn(string username, string password, Action<User> success, Action<string> error)
    {
        string url = Constants.API + "/accounts/auth/sign-in";

        using UnityWebRequest request = UnityWebRequest
            .Post(url, new Dictionary<string, string>
            {
                ["email"] = username,
                ["password"] = password,
            });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            error(request.error);
        }
        else
        {
            string jsonRes = request.downloadHandler.text;
            var authResponse = JsonUtility.FromJson<AuthResponse>(jsonRes);

            string[] jwt = authResponse.accessToken.Split(".");
            var user = JWT.DecodePayload<User>(jwt[1]);

            success(user);
        }
    }

    public void SaveUser(User user)
    {
        PlayerPrefs.SetString(Constants.USER, JsonUtility.ToJson(user));
    }

    public User GetUser()
    {
        var userJson = PlayerPrefs.GetString(Constants.USER);
        return JsonUtility.FromJson<User>(userJson);
    }

}
