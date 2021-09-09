using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider gateCollider;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.enabled = true;
            gateCollider.isTrigger = false;

        }
    }
}
