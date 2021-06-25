using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRHandAnim : MonoBehaviour
{
    private InputDevice targetDevice;
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = transform.GetComponent<Animator>();
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        foreach (var item in devices)
        { Debug.Log(item.name + item.characteristics); }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    // Update is called once per frame
    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue);
        _anim.SetFloat("Grip", gripValue);
        Debug.Log("grip: " + gripValue);
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        _anim.SetFloat("Trigger", triggerValue);




    }
}
