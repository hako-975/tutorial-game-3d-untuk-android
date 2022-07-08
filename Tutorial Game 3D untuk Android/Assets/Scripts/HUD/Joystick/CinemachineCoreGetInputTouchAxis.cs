using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCoreGetInputTouchAxis : MonoBehaviour
{
    public float touchSensitivity = 60f;

    TouchField touchField;

    // Start is called before the first frame update
    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
        touchField = FindObjectOfType<TouchField>();
    }

    float HandleAxisInputDelegate(string axisName)
    {
        switch (axisName)
        {
            case "Touch X":
                return touchField.touchDist.x / touchSensitivity;

            case "Touch Y":
                return touchField.touchDist.y / touchSensitivity;

            default:
                Debug.LogError("Input <" + axisName + "> tidak ada.", this);
                break;
        }

        return 0f;
    }
}
