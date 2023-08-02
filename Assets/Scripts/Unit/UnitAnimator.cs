using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;

    [SerializeField] private Transform rifleTransform;
    [SerializeField] private Transform swordTransform;

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
                    Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
                BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

                Vector3 targetUnitShootAtPosition = e.targetUnit.GetWorldPosition();
                targetUnitShootAtPosition.y = shootPointTransform.position.y;
                bulletProjectile.Setup(targetUnitShootAtPosition);
            };
        }
        if (TryGetComponent<SwordAction>(out SwordAction swordAction))
        {
            swordAction.OnSwordActionStarted += (sender, e) =>
            {
                EquipSword();
                animator.SetTrigger("SwordSlash");
            };
            swordAction.OnSwordActionCompleted += (sender, e) =>
            {
                EquipRifle();
            };
        }
    }

    private void Start()
    {
        EquipRifle();
    }

    private void EquipSword()
    {
        swordTransform.gameObject.SetActive(true);
        rifleTransform.gameObject.SetActive(false);
    }
    private void EquipRifle()
    {
        swordTransform.gameObject.SetActive(false);
        rifleTransform.gameObject.SetActive(true);
    }
}
