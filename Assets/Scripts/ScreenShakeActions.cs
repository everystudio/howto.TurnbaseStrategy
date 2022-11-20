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
        GrenadeProjectile.OnAnyGrenadeExploded += (sender, e) =>
        {
            ScreenShake.Instance.Shake(5f);
        };
        SwordAction.OnAnySwordHit += (sender, e) =>
        {
            //ScreenShake.Instance.Shake(2f);
        };
    }
}
