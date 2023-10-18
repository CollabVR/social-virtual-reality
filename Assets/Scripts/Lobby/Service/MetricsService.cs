using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;
using System.Text;

public class MetricsService
{
    public IEnumerator PostActivityAction(ActivityAction activityAction, Action<string> success, Action<string> error)
    {
        const string url = Constants.API + "/analytics/activity-action";

        var request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(activityAction));
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("PostActivityAction ERROR");
            string jsonRes = request.downloadHandler.text;
            error(jsonRes);
        }
        else
        {
            Debug.Log("PostActivityAction SUCCESS");

            string jsonRes = request.downloadHandler.text;
            success(jsonRes);
        }

    }

    public IEnumerator PostUserAction(UserAction userAction, Action<string> success, Action<string> error)
    {
        const string url = Constants.API + "/analytics/user-action";

        using UnityWebRequest request = UnityWebRequest
            .Post(url, new Dictionary<string, string>
            {
                ["userId"] = userAction.userId.ToString(),
                ["activityId"] = userAction.activityId.ToString(),
                ["timeSpeaking"] = userAction.timeSpeaking.ToString(),
            });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            string jsonRes = request.downloadHandler.text;
            error("Ocurrio un error inesperado");
        }
        else
        {
            string jsonRes = request.downloadHandler.text;
            success(jsonRes);
        }

    }
}