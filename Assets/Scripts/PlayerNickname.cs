using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNickname : MonoBehaviour
{
    public TMP_Text nickname;
    public Transform canvasNickname;
    public Transform XROriginTransform;

    // Start is called before the first frame update
    void Start()
    {
        nickname.text = "German";
    }

    // Update is called once per frame
    void Update()
    {
        canvasNickname.rotation = XROriginTransform.rotation;
    }


}
