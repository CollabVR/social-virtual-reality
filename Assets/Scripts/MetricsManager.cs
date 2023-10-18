using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricsManager : MonoBehaviour
{
    public static MetricsManager Instance;
    private MetricsService metricsService;


    public Activity currentActivity;
    public User currentUser;

    public ActivityAction activityAction;
    public UserAction userAction;
    public int userTimeSpeaking;

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

    void Start()
    {
        metricsService = new MetricsService();
        activityAction = new ActivityAction();
        userAction = new UserAction();
    }

    public void SendActivityActionsToServer(string action, int playerCount)
    {
        activityAction.activityId = currentActivity.id;
        activityAction.userId = currentUser.sub;
        activityAction.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        activityAction.action = action;
        activityAction.amountParticipants = playerCount;

        StartCoroutine(metricsService.PostActivityAction(activityAction,
            (jsonRes) => Debug.Log(jsonRes),
            (error) => Debug.Log(error)
        ));
    }

    public void SendUserActionsToServer()
    {
        userAction.userId = currentUser.sub;
        userAction.activityId = currentActivity.id;
        userAction.timeSpeaking = userTimeSpeaking;

        // metricsService.PostUserAction(
        //     userAction,
        //     (jsonRes) => Debug.Log("UserAction sent to server"),
        //     (error) => Debug.Log("Error sending UserAction to server")
        // );

    }



}
