using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCameraGameObject;

    private void Start()
    {
        BaseAction.OnAnyActionStarted += (sender, e) =>
        {
            switch (sender)
            {
                case ShootAction shootAction:
                    Unit shooterUnit = shootAction.GetUnit();
                    Unit targetUnit = shootAction.GetTargetUnit();
                    Vector3 cameraCharacterHeight = Vector3.up * 1.7f;

                    Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;

                    float shoulderOffsetAmount = 0.5f;
                    Vector3 shoulderOffet = Quaternion.Euler(0f, 90f, 0f) * shootDir * shoulderOffsetAmount;

                    Vector3 actionCameraPos =
                        shooterUnit.GetWorldPosition() +
                        cameraCharacterHeight +
                        shoulderOffet +
                        (shootDir * -1f);

                    actionCameraGameObject.transform.position = actionCameraPos;
                    actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);
                    ShowActionCamera();
                    break;
            }
        };

        BaseAction.OnAnyActionCompleted += (sender, e) =>
        {
            switch (sender)
            {
                case ShootAction shootAction:
                    HideActionCamera();
                    break;
            }
        };

        HideActionCamera();
    }

    private void ShowActionCamera()
    {
        actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCameraGameObject.SetActive(false);
    }

}
