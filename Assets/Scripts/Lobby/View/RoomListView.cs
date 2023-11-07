using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomListView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private RoomItem _roomItem;

    private List<RoomItem> roomItems = new List<RoomItem>();

    ActivityService activityService = new ActivityService();

    new void OnEnable()
    {
        ResetActiviesList();
        GetAndUpdateActivities();
    }

    public void updateRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList)
            {
                Debug.Log("Room removed: " + room.Name);
                int index = roomItems.FindIndex(x => x.RoomInfo.Name == room.Name);

                if (index != -1)
                {
                    Destroy(roomItems[index].gameObject);
                    roomItems.RemoveAt(index);
                }
            }
            else
            {
                Debug.Log("Room created: " + room.Name);
                RoomItem roomItem = Instantiate(_roomItem, _content);
                roomItem.SetRoomInfo(room);
                roomItems.Add(roomItem);
            }
        }
    }

    private void ResetActiviesList()
    {
        foreach (RoomItem roomItem in roomItems)
        {
            Destroy(roomItem.gameObject);
        }
        roomItems.Clear();
    }

    public void UpdateActivitiesList(List<Activity> activities)
    {
        Activity activity1 = new Activity();
        activity1.name = "Dev Scene";
        activity1.maxParticipants = 20;
        activity1.environmentId = "Dev Scene";
        activities.Add(activity1);

        Activity activity2 = new Activity();
        activity2.name = "Environment 1";
        activity2.maxParticipants = 20;
        activity2.environmentId = "Environment 1";
        activities.Add(activity2);

        Activity activity3 = new Activity();
        activity3.name = "Environment 2";
        activity3.maxParticipants = 20;
        activity3.environmentId = "Environment 2";
        activities.Add(activity3);

        Activity activity4 = new Activity();
        activity4.name = "Environment 3";
        activity4.maxParticipants = 20;
        activity4.environmentId = "Environment 3";
        activities.Add(activity4);

        foreach (Activity activity in activities)
        {
            RoomItem roomItem = Instantiate(_roomItem, _content);
            roomItem.SetActivityInfo(activity);
            roomItems.Add(roomItem);
        }
    }

    private void GetAndUpdateActivities()
    {
        // temp:
        UpdateActivitiesList(new List<Activity>());

        StartCoroutine(activityService.GetActivities(
            success: (res) =>
            {
                UpdateActivitiesList(res);
            },
            error: (e) =>
            {
                Debug.Log("Error al cargar las actividades");
            }
        ));
    }

}