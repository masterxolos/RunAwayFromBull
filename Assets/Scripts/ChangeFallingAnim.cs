using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFallingAnim : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator animator;

    public void FallAnimaton()
    {

        
        animator.SetBool("HasEntered", true);
    }
}
