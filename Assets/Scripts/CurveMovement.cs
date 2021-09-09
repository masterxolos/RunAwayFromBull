﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    protected float Animation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Animation += Time.deltaTime;
        Animation = Animation % 5f;

        transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward * 10f, 5f, Animation / 5f);
    }
}
