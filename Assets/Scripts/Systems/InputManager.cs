#define USE_NEW_INPUT_SYSTEM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputActions playerInputActions;
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
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMouseScreenPosition()
    {
#if USE_NEW_INPUT_SYSTEM
        return Mouse.current.position.ReadValue();
#else
        return Input.mousePosition;
#endif
    }

    public bool IsMouseButtonDownThisFrame()
    {
#if USE_NEW_INPUT_SYSTEM
        return playerInputActions.Player.Click.WasPressedThisFrame();
#else
        return Input.GetMouseButtonDown(0);
#endif
    }

    public Vector3 GetCameraMoveVector()
    {
        Vector3 inputMoveDir = Vector3.zero;

#if USE_NEW_INPUT_SYSTEM

        Vector2 tempInput = playerInputActions.Player.CameraMovement.ReadValue<Vector2>();

        inputMoveDir.x = tempInput.x;
        inputMoveDir.z = tempInput.y;
#else
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
#endif
        return inputMoveDir;
    }

    public Vector3 GetCameraRotateVector()
    {
        Vector3 rotationVector = Vector3.zero;
#if USE_NEW_INPUT_SYSTEM

        rotationVector.y = playerInputActions.Player.CameraRotate.ReadValue<float>();

#else
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }
#endif
        return rotationVector;
    }

    public float GetCameraZoomAmount()
    {
#if USE_NEW_INPUT_SYSTEM
        return playerInputActions.Player.CameraZoom.ReadValue<float>();
#else
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
#endif
    }


}
