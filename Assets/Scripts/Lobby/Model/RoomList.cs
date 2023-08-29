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

    public void updateRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList)
            {
                Debug.Log("Room removed: " + room.Name);
                int index = roomItems.FindIndex(x => x.RoomInfo.Name == room.Name);
                Debug.Log("Room index: " + index);

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

}