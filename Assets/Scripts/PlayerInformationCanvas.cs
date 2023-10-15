using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Voice.PUN;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformationCanvas : MonoBehaviour
{
    public TMP_Text nickname;
    // public Image speakerImage;
    public Transform speakerImage;

    public Transform canvasPlayerInformation;

    public Transform body;
    private PhotonView photonView;
    private PhotonVoiceView photonVoiceView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        photonVoiceView = GetComponent<PhotonVoiceView>();
    }

    void Update()
    {
        canvasPlayerInformation.rotation = body.rotation;
        SetSpeakerImage();

        if (photonView.Owner == null) return;
        if (nickname.text == photonView.Owner.NickName) return;
        
        nickname.text = photonView.Owner.NickName;
    }

    public void SetSpeakerImage()
    {
        Debug.Log("SetSpeakerImage");
        if (!photonView.IsMine) return;
        
        speakerImage.transform.localScale = new Vector3(1, photonVoiceView.IsRecording ? 1 : 0, 1);
    }

}
