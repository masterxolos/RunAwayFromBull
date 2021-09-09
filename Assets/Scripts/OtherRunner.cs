using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OtherRunner : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private Animator animator;
    private void Start()
    {
       
    }
    private void Update()
    {
        if(!animator.GetBool("HasEntered"))
            transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
           
            animator.SetBool("HasEntered", true);
    }
    
}
