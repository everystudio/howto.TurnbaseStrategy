using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPoint;
    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += (sender, e) =>
            {
                animator.SetBool("IsWalking", true);
            };
            moveAction.OnStopMoving += (sender, e) =>
            {
                animator.SetBool("IsWalking", false);
            };
        }
        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += (sender, e) =>
            {
                animator.SetTrigger("Shoot");
                Transform bulletProjectileTransform =
                    Instantiate(bulletProjectilePrefab, shootPoint.position, Quaternion.identity);
                BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

                Vector3 targetUnitShootAtPosition = e.targetUnit.GetWorldPosition();
                targetUnitShootAtPosition.y = shootPoint.position.y;
                bulletProjectile.Setup(targetUnitShootAtPosition);
            };
        }
    }
}
