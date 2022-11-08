using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
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
            };
        }
    }
}
