using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public Vector2 GetMouseScreenPosition()
    {
        return Input.mousePosition;
    }

    public bool IsMouseButtonDown()
    {
        return Input.GetMouseButtonDown(0);
    }

    public Vector3 GetCameraMoveVector()
    {
        Vector3 inputMoveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }
        return inputMoveDir;
    }

    public Vector3 GetCameraRotateVector()
    {
        Vector3 rotationVector = Vector3.zero;
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }
        return rotationVector;
    }

    public float GetCameraZoomAmount()
    {
        float zoomAmount = 1f;
        if (0 < Input.mouseScrollDelta.y)
        {
            zoomAmount *= -1f;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            zoomAmount *= +1f;
        }
        else
        {
            zoomAmount = 0;
        }
        return zoomAmount;
    }


}
