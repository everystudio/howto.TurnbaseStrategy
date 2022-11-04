using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : MonoBehaviour
{
    private bool startSpining;
    private void Update()
    {
        if (startSpining)
        {
            float spinAddAmount = 360f * Time.deltaTime;
            transform.eulerAngles += Vector3.up * spinAddAmount;
        }
    }
    public void Spin()
    {
        startSpining = true;
    }
}
