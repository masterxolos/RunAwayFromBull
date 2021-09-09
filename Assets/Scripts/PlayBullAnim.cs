using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBullAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animation bullAnim;

    [SerializeField] private AnimationClip[] clips = new AnimationClip[2];

    public void BullAttack()
    {
        bullAnim.clip = clips[1];
        bullAnim.Play();
        
        
    }
}
