using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptController : MonoBehaviour
{
    
    [SerializeField] private Movement movement;
    [SerializeField] private BullMovement bullMovement;
    [SerializeField] private ParabolaController objectToFollow;
    [SerializeField] private int rotationSpeed = 5;
    public bool shouldWork = false;
    public bool shouldGoRight = false;
    public bool shouldGoForward = true;
    public bool shouldGoLeft = false;
    private float _normal = 0;

    public float Normal
    {
        get => _normal;
        set => _normal = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turn"))
        {
            movement.enabled = false;
            objectToFollow.enabled = true;
            shouldWork = true;
            shouldGoForward = false;
            shouldGoLeft = false;
            shouldGoRight = true;
            
        }
        else if (other.CompareTag("StopTurn"))
        {
            _normal = objectToFollow.gameObject.transform.position.z;
            movement.enabled = true;
            shouldWork = false;
            objectToFollow.enabled = true;
        }
    }
    
    private void Update()
    {
        if (shouldWork == true)
        {
            Vector3 targetDirection = objectToFollow.gameObject.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            transform.position += transform.forward * rotationSpeed * Time.deltaTime;
        }
    }
}
