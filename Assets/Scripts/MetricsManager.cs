using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricsManager : MonoBehaviour
{
    public static MetricsManager Instance;
    
    public Activity currentActivity;
    public User currentUser;

    public int playerCount;
    public float averageActivityDuration;
    public float averageUserConnection;

    public Dictionary<string, int> microphoneUsageFrequency = new Dictionary<string, int>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para registrar la duración de una actividad.
    public void LogActivityDuration(float duration)
    {
        averageActivityDuration += duration;
    }

    // Método para registrar la conexión de un usuario.
    public void LogUserConnection(float connectionTime)
    {
        averageUserConnection += connectionTime;
    }

    // Método para registrar el uso del micrófono por actividad.
    public void LogMicrophoneUsage(string activityName)
    {
        if (microphoneUsageFrequency.ContainsKey(activityName))
        {
            microphoneUsageFrequency[activityName]++;
        }
        else
        {
            microphoneUsageFrequency.Add(activityName, 1);
        }
    }

    // Método para enviar las métricas al servidor a través de la API.
    public void SendMetricsToServer()
    {
        // lógica para enviar las métricas al servidor a través de la API.
        // Utiliza HTTP o cualquier protocolo adecuado para tu caso.
    }

    void OnApplicationQuit()
    {
        SendMetricsToServer();
    }

}
