using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingCheck : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("HasEntered", true);

        }
    }



}
