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

        var request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(userAction));
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("PostUserAction ERROR");
            string jsonRes = request.downloadHandler.text;
            error(jsonRes);
        }
        else
        {
            Debug.Log("PostUserAction SUCCESS");
            string jsonRes = request.downloadHandler.text;
            success(jsonRes);
        }
    }
}