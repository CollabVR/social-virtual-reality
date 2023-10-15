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
    public Transform mouth;

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


        if (photonVoiceView.IsRecording)
        {
            mouth.transform.localScale = new Vector3(x: 0.06f, y: 0.03f, z: 0.006f);
        }
        else
        {
            mouth.transform.localScale = new Vector3(x: 0.06f, y: 0f, z: 0f);
        }

        // var MicrophoneDevice = photonVoiceView.RecorderInUse.MicrophoneDevice;
        // Debug.Log("Microphone device: " + MicrophoneDevice.Name);

        // // microphone devices
        // foreach (var device in Microphone.devices)
        // {
        //     Debug.Log("Name: " + device);
        // }
    }

}
