using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNickname : MonoBehaviour
{
    public TMP_Text nickname;
    public Transform canvasNickname;
    public Transform body;

    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        canvasNickname.rotation = body.rotation;

        if (photonView.Owner == null) return;
        if (nickname.text == photonView.Owner.NickName) return;
        
        nickname.text = photonView.Owner.NickName;
    }

}
