using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using UnityEngine.SocialPlatforms;
using Photon.Voice;
using System.Security.Cryptography.X509Certificates;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField _inputField;
    
    public float distance = 0.5f;
    public float verticalOffset = -0.5f;

    public Transform positionSource;

    private bool _isOpen = false;

    void Start() {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onSelect.AddListener(x => OpenKeyboard());
        // _inputField.onDeselect.AddListener(x => FinishedEditing("on deselect"));
        // _inputField.onEndEdit.AddListener(x => FinishedEditing("on end edit"));
        // _inputField.onSubmit.AddListener(x => FinishedEditing("on submit"));
    }

    void Update() {
        if (_isOpen) {
            _inputField.text = NonNativeKeyboard.Instance.InputField.text;
        }
    }

    public void FinishedEditing(string text) {
        Debug.Log("fisished Editing: " + text);
        _isOpen = false;
        string inputText = _inputField.text;
        _inputField.text = inputText.ToString();
    }

    public void OpenKeyboard() {
        // NonNativeKeyboard.Instance.InputField = _InputField;
        _isOpen = true;

        NonNativeKeyboard.Instance.PresentKeyboard(_inputField.text);

        KeyboardPosition();
    }

    void KeyboardPosition() {
        Vector3 direction = positionSource.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPosition = positionSource.position + direction * distance + Vector3.up * verticalOffset;

        NonNativeKeyboard.Instance.RepositionKeyboard(targetPosition);
    }
}
