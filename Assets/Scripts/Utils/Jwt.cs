using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class JWT
{

    public static Dictionary<string, string> DecodePayload(string payload)
    {
        string decodedPayload = Base64UrlDecode(payload);

        Dictionary<string, string> payloadData = new Dictionary<string, string>();
        string[] payloadFields = decodedPayload.Split(',');

        foreach (string field in payloadFields)
        {
            string[] keyValue = field.Split(':');

            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim().Trim('"');
                string value = keyValue[1].Trim().Trim('"');
                payloadData[key] = value;
            }
        }

        return payloadData;
    }

    private static string Base64UrlDecode(string input)
    {
        Debug.Log("Input " + input);
        string modifiedInput = input.Replace('-', '+').Replace('_', '/');
        int padding = 4 - (modifiedInput.Length % 4);
        if (padding == 4)
        {
            padding = 0;
        }
        modifiedInput += new string('=', padding);

        Debug.Log("Modified: " + modifiedInput);
        byte[] data = Convert.FromBase64String(modifiedInput);
        Debug.Log("Data: " + data);
        return Encoding.UTF8.GetString(data);
    }

    public static T DecodePayload<T>(string payload)
    {
        string decodedPayload = Base64UrlDecode(payload);
        return JsonUtility.FromJson<T>(decodedPayload);
    }
}