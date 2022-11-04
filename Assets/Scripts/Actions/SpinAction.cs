using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;
    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += Vector3.up * spinAddAmount;

        totalSpinAmount += spinAddAmount;
        if (360f <= totalSpinAmount)
        {
            isActive = false;
        }
    }
    public void Spin()
    {
        isActive = true;
        totalSpinAmount = 0f;
    }
}
