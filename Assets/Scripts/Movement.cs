using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Tabtale.TTPlugins;

public class Movement : MonoBehaviour
{
    //[SerializeField] PlayerManager playerManager;

    [SerializeField] Transform mainCharTransform;
    //Touch Settings
    [SerializeField] bool isTouching;
    float touchPosX;
    float touchPosZ;
    Vector3 direction;
    [SerializeField, ReadOnly] private float movementSpeed;
    [SerializeField, ReadOnly] private float controlSpeed;

    [SerializeField] private Animator animator;
    [SerializeField] private ScriptController scriptController;

    [SerializeField] private BullMovement bullMovement;
    [SerializeField] private Movement _movement;
    [SerializeField] private GameObject Maincamera;
    [SerializeField] private Transform cameraTempParent;

    [SerializeField] private GameObject failCanvas;

    private bool powerUpMode;

    public bool increaseBullSpeed;

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        //playerManager.playerState == PlayerManager.PlayerState.Move
        if (true)
        {
            if (scriptController.shouldGoForward)
            {
                transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, 0, 0)), Time.deltaTime * movementSpeed);
                
                if (isTouching)
                {
                    touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
                }

                transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
            }
            
            else if (scriptController.shouldGoRight)
            {
                transform.position += Vector3.right * movementSpeed * Time.fixedDeltaTime;
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(90,Vector3.up), Time.deltaTime * movementSpeed);
                
                if (isTouching)
                {
                    touchPosZ += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
                }

                transform.position = new Vector3(transform.position.x, transform.position.y, scriptController.Normal -touchPosZ);
                
            }
            else if (scriptController.shouldGoLeft)
            {
                Debug.Log("c");
                transform.position += Vector3.left * movementSpeed * Time.fixedDeltaTime;
            }
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
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("stopPlayer"))
        {
            
            _movement.enabled = false;
            Maincamera.transform.parent = cameraTempParent;
            animator.SetBool("Dance", true);
            
            mainCharTransform.DORotate(new Vector3(0,300,0), 1);
            Debug.Log("Döndü");

        }
        if (collider.CompareTag("PowerUp"))
        {
            Destroy(collider.gameObject);
            StartCoroutine(WaitForPowerUp());
        }
        if (collider.CompareTag("Obstacle"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            StartCoroutine(waitForaBit());
            animator.SetBool("hasFallen", true);
            _movement.enabled = false;
            Maincamera.transform.parent = cameraTempParent;
            
        }
        if (collider.CompareTag("Decreaser"))
        {
            bullMovement.increaseBullSpeed();
            Maincamera.transform.parent = cameraTempParent;
        }
        if (collider.CompareTag("Bulls"))
        {
            StartCoroutine(waitForaBit());
            animator.SetBool("hasFallen", true);
            _movement.enabled = false;
            Maincamera.transform.parent = cameraTempParent;
        }
        if (collider.CompareTag("Jogging"))
        {
            animator.SetBool("Bumped", true);
            _movement.enabled = false;
            Maincamera.transform.parent = cameraTempParent;
        }
        if (collider.CompareTag("Bariers"))
        {
            Maincamera.transform.parent = cameraTempParent;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bariers"))
        {
            Maincamera.transform.parent = cameraTempParent;
           
        }

    }

    IEnumerator WaitForPowerUp()
    {
        movementSpeed += 5;
        powerUpMode = true;
        
        yield return new WaitForSeconds(2);

        powerUpMode = false;
        movementSpeed -= 5;
        bullMovement.increaseBullSpeed();
    }

    IEnumerator waitForaBit()
    {
        Debug.Log("a");
        yield return new WaitForSeconds(2);
        failCanvas.SetActive(true);
    }

    private void Awake()

    {

        // Initialize CLIK

        TTPCore.Setup();

        // Your code here

    }
}
