using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private float _differenceX;
    private float _lastFrameFingerPosX;

    public float GetMovementValue()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPosX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _differenceX = Input.mousePosition.x - _lastFrameFingerPosX;
            _lastFrameFingerPosX = Input.mousePosition.x;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            _differenceX = 0f;
        }
        return _differenceX;
    }
}
