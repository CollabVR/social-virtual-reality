using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;

public class InputManager : MonoBehaviour
{
    PhotonView photonView;

    public InputDevice _rightController;
    public InputDevice _leftController;
    public InputDevice _HMD;

    // variables para emular ButtonDownPressed
    private bool previousRightPrimaryButtonState = false;


    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (!_rightController.isValid || !_leftController.isValid || !_HMD.isValid)
            InitializeInputDevices();
    }
    private void InitializeInputDevices()
    {

        if (!_rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);
        if (!_leftController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref _leftController);
        if (!_HMD.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref _HMD);

    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }

    public bool RightPrimaryButtonPressed()
    {
        _rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButton);
        return rightPrimaryButton;
    }

    public bool RightPrimaryButtonDownPressed()
    {
        bool isPressed = false;
        if (_rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButton))
        {
            if (rightPrimaryButton && !previousRightPrimaryButtonState)
            {
                isPressed = true;
            }
            previousRightPrimaryButtonState = rightPrimaryButton;
        }
        return isPressed;
    }

    public bool RightSecondaryButtonPressed()
    {
        _rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool rightSecondaryButton);
        return rightSecondaryButton;
    }


    public bool LeftPrimaryButtonPressed()
    {
        _leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButton);
        return leftPrimaryButton;
    }

    public bool LeftSecondaryButtonPressed()
    {
        _leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool leftSecondaryButton);
        return leftSecondaryButton;
    }

}
