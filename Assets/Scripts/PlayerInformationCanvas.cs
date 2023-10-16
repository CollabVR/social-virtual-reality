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
    public float mouthScale = 0.03f;

    public Transform speakerImage;

    public Transform canvasPlayerInformation;

    public Transform body;
    private PhotonView photonView;
    private PhotonVoiceView photonVoiceView;
    private VoiceManager voiceManager;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        photonVoiceView = GetComponent<PhotonVoiceView>();
        voiceManager = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
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
        if (!photonView.IsMine) return;

        speakerImage.transform.localScale = new Vector3(1, photonVoiceView.IsRecording ? 1 : 0, 1);


        if (photonVoiceView.IsRecording)
        {
            Debug.Log("Voice Manager: " + voiceManager.GetLoudnessFromMicrophone());
            float loudness = voiceManager.GetLoudnessFromMicrophone();
            float yMax = 0.04f;

            if (loudness * mouthScale > yMax) loudness = yMax;
            mouth.transform.localScale = new Vector3(x: 0.06f, y: loudness * mouthScale, z: 0.006f);
            // mouth.transform.localScale = new Vector3(x: 0.06f, y: 0.03f, z: 0.006f);
        }
        else
        {
            mouth.transform.localScale = new Vector3(x: 0.06f, y: 0f, z: 0f);
        }
    }

}
