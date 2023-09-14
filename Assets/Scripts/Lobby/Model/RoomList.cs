using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private RoomItem _roomItem;

    private List<RoomItem> roomItems = new List<RoomItem>(); // _listings

    ActivityService activityService = new ActivityService();

    void Start()
    {
        Debug.Log("Get Activities");
        GetActivities();
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
                RoomItem roomItem = Instantiate(_roomItem, _content); // _listing
                roomItem.SetRoomInfo(room);
                roomItems.Add(roomItem);
            }
        }
    }

    void GetActivities()
    {
        StartCoroutine(activityService.GetActivities(
        success: (user) =>
        {

        },
        error: (error) =>
        {

        }
        ));

    }

    public void UpdateActivitiesList(List<Activity> activities)
    {
        foreach (Activity activity in activities)
        {
            RoomItem roomItem = Instantiate(_roomItem, _content);
            roomItem.SetActivityInfo(activity);
            roomItems.Add(roomItem);
        }
    }

}