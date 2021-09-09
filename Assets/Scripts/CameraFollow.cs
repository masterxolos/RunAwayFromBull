using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    //[SerializeField] PlayerManager playerManager;
    [SerializeField] Transform camTransform;
    [SerializeField] int movementTime;


    //private CollectedObjController CollectedObjScript;

    public Transform target;
    [SerializeField, ReadOnly] private Vector3 offset;

    [SerializeField] float smoothSpeed;
    // private Vector3 offset = new Vector3(0, 10, 0);

    private bool everyoneJumped;
    private void Start()
    {
        //CollectedObjScript = CollectedObjController.GetComponent<CollectedObjController>();
    }

    private void Update()
    {
        //everyoneJumped = CollectedObjScript.getEveryoneJumpedValue();
        
    }

    void LateUpdate()
    {
        //playerManager.levelState == PlayerManager.LevelState.NotFinished
        //   offset = offset + new Vector3(0, 0, GameSettings.Instance().ZOffset); 
       
         Vector3 desiredPos = target.position + offset;
         Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
         transform.position = new Vector3(transform.position.x, transform.position.y, smoothedPos.z);
    }
}

    