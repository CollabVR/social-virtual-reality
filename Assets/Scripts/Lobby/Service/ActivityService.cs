using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;
using System.Text;

public class ActivityService
{
    public IEnumerator GetActivities(Action<List<Activity>> success, Action<string> error)
    {
        using UnityWebRequest request = UnityWebRequest.Get(Constants.API + "/activities");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.result);
        }
        else
        {
            Debug.Log(request.result);
            string jsonRes = request.downloadHandler.text;
            Debug.Log(jsonRes);

            var res = JsonUtility.FromJson<ActivityResponse>("{\"activities\":" + jsonRes + "}");
            success(res.activities);
        }
    }

}
