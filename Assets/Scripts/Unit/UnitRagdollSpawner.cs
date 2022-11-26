using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform ragdollPrefab;
    [SerializeField] private Transform originalRootBone;

    private Healthsystem healthsystem;

    private void Awake()
    {
        healthsystem = GetComponent<Healthsystem>();
        healthsystem.OnDead += Healthsystem_OnDead;
    }

    private void Healthsystem_OnDead(object sender, System.EventArgs e)
    {
        Transform ragdollTransform = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        UnitRagdoll unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();
        unitRagdoll.Setup(originalRootBone);
    }
}
