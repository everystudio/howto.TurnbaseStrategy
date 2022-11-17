using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeActions : MonoBehaviour
{
    private void Start()
    {
        ShootAction.OnAnyShoot += (sender, e) =>
        {
            ScreenShake.Instance.Shake();
        };
    }
}
