using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AuthService
{
    public IEnumerator Login(string username, string password, Action<UserAuth> success, Action<string> error) {
        string url = Constants.API + "/jokes/random";

        using(UnityWebRequest request = UnityWebRequest.Get(url)) {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) {
                error(request.error);
            }
            
            string json = request.downloadHandler.text;
            UserAuth userAuth = JsonUtility.FromJson<UserAuth>(json);
            success(userAuth);
        }
    }

}
