using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HavaiFisekDoTween : MonoBehaviour
{
    [SerializeField] private Transform fireworksTransform;

    private void Start()
    {
        fireworksTransform.DOMoveY(10, 5);
    }
}
