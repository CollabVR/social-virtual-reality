using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using Photon.Voice.Unity.UtilityScripts;

public class VoiceManager : MonoBehaviour
{
    Recorder recorder;
    PunVoiceClient voiceClient;
    MicAmplifier micAmplifier;

    void Start()
    {
        voiceClient = GetComponent<PunVoiceClient>();
        recorder = GetComponent<Recorder>();
        micAmplifier = GetComponent<MicAmplifier>();
    }

    void Update()
    {

    }

    public float GetLoudnessFromMicrophone()
    {
        return recorder.LevelMeter.CurrentAvgAmp;
    }

    public void SetMicrophoneVolume(float volume)
    {
        // volume is between 0 and 2.5
        micAmplifier.AmplificationFactor = volume;
    }

    public void MutePlayer()
    {
        Debug.Log("MutePlayer");
        recorder.RecordingEnabled = !recorder.RecordingEnabled;
    }


}
