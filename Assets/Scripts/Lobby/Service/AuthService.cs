using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;
using System.Text;

public class AuthService
{
    public IEnumerator SignIn(string username, string password, Action<bool> success, Action<string> error)
    {
        string url = Constants.API + "/accounts/auth/sign-in";

        using UnityWebRequest request = UnityWebRequest.Post(url, 
            new Dictionary<string, string>
            {
                ["email"] = username,
                ["password"] = password,
            });

        yield return request.SendWebRequest();

        // response
        Debug.Log("Request Result: " + request.result);

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Response Error");
            error(request.error);
        }
        else
        {
            Debug.Log("Response Success");
            string jsonRes = request.downloadHandler.text;
            var authResponse = JsonUtility.FromJson<AuthResponse>(jsonRes);


            string[] jwt = authResponse.accessToken.Split(".");

            var payloadData = JWT.DecodePayload(jwt[1]);

            Debug.Log(payloadData["email"]);
        }
    }

    private static string Base64UrlDecode(string input)
    {
        string modifiedInput = input.Replace('-', '+').Replace('_', '/');
        int padding = 4 - (modifiedInput.Length % 4);
        modifiedInput += new string('=', padding);

        byte[] data = Convert.FromBase64String(modifiedInput);
        return Encoding.UTF8.GetString(data);
    }

}
