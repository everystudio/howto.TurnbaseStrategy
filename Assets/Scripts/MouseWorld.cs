using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance;

    [SerializeField] private LayerMask groundLayerMask;

    private void Awake()
    {
        Instance = this;
    }
    /*
    private void Update()
    {
        transform.position = GetMousePosition();
    }
    */
    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        bool bIsHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.groundLayerMask);
        return raycastHit.point;
    }
}
