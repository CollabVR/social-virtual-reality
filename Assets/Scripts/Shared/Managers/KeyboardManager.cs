using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Photon.Pun.Demo.Cockpit;
using TMPro;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    [Header("PlayerMNKController (Player)")]
    public PlayerMNKController MNKController;

    [Header("Keyboard Position")]
    public Transform positionSource;
    public float distance = 0.5f;
    public float verticalOffset = -0.5f;

    [Header("References To Input Fields")]
    public List<TMP_InputField> inputFields;
    private TMP_InputField currentInputField; 

    void Start()
    {
        foreach (var inputField in inputFields)
        {
            inputField.onSelect.AddListener(x => OpenKeyboard(inputField));
        }
    }

    void Update()
    {
        if (!MNKController.usingVR) return;
        if (currentInputField == null) return;

        currentInputField.text = NonNativeKeyboard.Instance.InputField.text;
    }

    private void OpenKeyboard(TMP_InputField inputField) {
        if (!MNKController.usingVR) return;

        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
        currentInputField = inputField;

        KeyboardPosition();
    }

    private void KeyboardPosition() {
        Vector3 direction = positionSource.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPosition = positionSource.position + direction * distance + Vector3.up * verticalOffset;

        NonNativeKeyboard.Instance.RepositionKeyboard(targetPosition);
    }
}
