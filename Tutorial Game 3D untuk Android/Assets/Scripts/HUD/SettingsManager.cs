using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider cameraZoomSlider;
    public Slider sensitivitySlider;

    CinemachineCoreGetInputTouchAxis touchSensitivity;
    CinemachineFreeLook cinemachineFreeLook;

    // Start is called before the first frame update
    void Start()
    {
        touchSensitivity = FindObjectOfType<CinemachineCoreGetInputTouchAxis>();
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
    }

    public void SetCameraZoomSlider(float cameraZoom)
    {
        cinemachineFreeLook.m_CommonLens = true;
        cinemachineFreeLook.m_Lens.FieldOfView = cameraZoom;
        PlayerPrefsManager.instance.SetCameraZoom(cameraZoom);
    }

    public void SetSensitivitySlider(float sensitivity)
    {
        touchSensitivity.touchSensitivity = sensitivity;
        PlayerPrefsManager.instance.SetSensitivity(sensitivity);
    }    

    public void ResetSettings()
    {
        PlayerPrefsManager.instance.DeleteKey("CameraZoom");
        PlayerPrefsManager.instance.DeleteKey("Sensitivity");
    }

    // Update is called once per frame
    void Update()
    {
        cameraZoomSlider.value = PlayerPrefsManager.instance.GetCameraZoom();
        sensitivitySlider.value = PlayerPrefsManager.instance.GetSensitivity();

        SetCameraZoomSlider(cameraZoomSlider.value);
    }
}
