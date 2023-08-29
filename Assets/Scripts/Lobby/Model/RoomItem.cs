using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

// RoomListing
public class RoomItem : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo) 
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name + " ";
    }

    public void OnClickButton()
    {
        Debug.Log("Selected Room: " + RoomInfo.Name);
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }

}
