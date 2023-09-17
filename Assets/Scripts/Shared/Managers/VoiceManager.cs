using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;

public class VoiceManager : MonoBehaviour
{
    Recorder recorder;

    void Start()
    {
        recorder = GetComponent<Recorder>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            recorder.RecordingEnabled = !recorder.RecordingEnabled;
        }
    }
}
