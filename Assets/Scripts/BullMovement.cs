using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BullMovement : MonoBehaviour
{
    //Touch Settings
    [SerializeField] bool isTouching;
    float touchPosX;
    float touchPosZ;
    Vector3 direction;
    [SerializeField] private float bullSpeed;
    [SerializeField] private float controlSpeed;

    public Movement movement;
    public GameObject FastRun;
    
    [SerializeField] private ScriptController scriptController;

    private bool isIncreased;

    [SerializeField] private GameObject bullLocation1;
    [SerializeField] private GameObject bull;

    public bool shouldGoForward = true;
    public bool shouldGoRight = false;
    public bool shouldGoLeft= false;
    public bool shouldWork= false;
    public bool movementEnabled= true;
    
    [SerializeField] private ParabolaController objectToFollow;
    [SerializeField] private GameObject objectToFollowAlways;


    [SerializeField] private Collider bullCollider;
    [SerializeField] private BullMovement bullMovement;
    [SerializeField] private PlayBullAnim playBullAnim1;
    [SerializeField] private PlayBullAnim playBullAnim2;
    [SerializeField] private PlayBullAnim playBullAnim3;
    [SerializeField] private GameObject bull_1;
    [SerializeField] private GameObject bull_2;
    [SerializeField] private GameObject bull_3;
    private float normal = 0;
    void Start()
    {
        movement = FastRun.GetComponent<Movement>();
        playBullAnim1 = bull_1.GetComponent<PlayBullAnim>();
        playBullAnim2 = bull_2.GetComponent<PlayBullAnim>();
        playBullAnim3 = bull_3.GetComponent<PlayBullAnim>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            bullMovement.enabled = false;
        }
        if (other.CompareTag("Turn"))
        {
            movementEnabled = false;
            shouldGoForward = false;
            shouldGoLeft = false;
            shouldGoRight = true;
            shouldWork = true;
            objectToFollow.enabled = true;
        }
        
        else if (other.CompareTag("StopTurn"))
        {
            normal = objectToFollow.gameObject.transform.position.z;
            movementEnabled = true;
            shouldWork = false;
            objectToFollow.enabled = false;
        }
        if (other.CompareTag("Player"))
        {
            //play attack anim
            //bullMovement.enabled = false;
            bullSpeed = 3;
            playBullAnim1.BullAttack();
            playBullAnim2.BullAttack();
            playBullAnim3.BullAttack();
        }
        if (other.CompareTag("Gate"))
        {
            playBullAnim1.BullAttack();
            playBullAnim2.BullAttack();
            playBullAnim3.BullAttack();
        }

    }

    
    
    void Update()
    {
        GetInput();
        if (shouldWork)
        {
            Vector3 targetDirection = objectToFollow.gameObject.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
            transform.position += transform.forward * 5* Time.deltaTime;
        }
    }
    
    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
        }
        else
        {
            isTouching = false;
        }
    }
    private void FixedUpdate()
    {
        /*
        //playerManager.playerState == PlayerManager.PlayerState.Move
        if (true)
        {
            if (scriptController.shouldGoForward)
            {
                transform.position += Vector3.forward * bullSpeed * Time.fixedDeltaTime;
            }
            if (shouldGoRight)
            {
                transform.position += Vector3.right * bullSpeed * Time.fixedDeltaTime;
               // transform.position += Vector3.right * bullSpeed * Time.fixedDeltaTime;
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        */
        
        if (movementEnabled)
        {
            if (shouldGoForward)
            {
                transform.position += Vector3.forward * bullSpeed * Time.fixedDeltaTime;
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(objectToFollowAlways.transform.position), Time.deltaTime * bullSpeed);
                
                if (isTouching)
                {
                    touchPosX += Input.GetAxis("Mouse X") * 3 * Time.fixedDeltaTime;
                }

                transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
            }
            
            else if (shouldGoRight)
            {
                transform.position += Vector3.right * bullSpeed * Time.fixedDeltaTime;
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(90,Vector3.up), Time.deltaTime * bullSpeed);
                
                if (isTouching)
                {
                    touchPosZ += Input.GetAxis("Mouse X") * 3 * Time.fixedDeltaTime;
                }

                transform.position = new Vector3(transform.position.x, transform.position.y, normal-touchPosZ );
                
            }
            else if (scriptController.shouldGoLeft)
            {
                Debug.Log("c");
                transform.position += Vector3.left * bullSpeed * Time.fixedDeltaTime;
            }
        }
    }
    
    public void changePosition()
    {
        Debug.Log("position changed");
        gameObject.transform.position = bullLocation1.transform.position;
        transform.Rotate(0,90,0);
        shouldGoRight = true;
    }

    public void increaseBullSpeed()
    {
        isIncreased = true;
        StartCoroutine(IncreaseBullSpeed());
        
    }

    private IEnumerator IncreaseBullSpeed()
    {
        if (isIncreased == true)
        {
            bullSpeed += 5;
           
            yield return new WaitForSeconds(2f);
            if (bullSpeed > 3)
            {
                bullSpeed -= 5;
            }
           
            
            isIncreased = false;
        }
        
    }
}

   
