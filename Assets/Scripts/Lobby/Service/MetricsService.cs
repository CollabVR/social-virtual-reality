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
        const string url = Constants.API + "/metrics/activity-action";

        using UnityWebRequest request = UnityWebRequest
            .Post(url, new Dictionary<string, string>
            {
                ["activityId"] = activityAction.activityId.ToString(),
                ["userId"] = activityAction.userId.ToString(),
                ["userId"] = activityAction.timestamp,
                ["action"] = activityAction.action,
                ["amountParticipants"] = activityAction.amountParticipants.ToString(),
            });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            string jsonRes = request.downloadHandler.text;
            success(jsonRes);
        }
        else
        {
            error("Ocurrio un error inesperado");
        }

    }

    public IEnumerator PostUserAction(UserAction userAction, Action<string> success, Action<string> error)
    {
        const string url = Constants.API + "/metrics/user-action";

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
            success(jsonRes);
        }
        else
        {
            error("Ocurrio un error inesperado");
        }
        
    }
}