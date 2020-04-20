﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleItem : MonoBehaviour
{
    [SerializeField]
    private bool isIdle;

    [SerializeField]
    private bool isUp = false;

    private float step = 0.002f;

    private float maxHeight = -2.75f;
    private float minHeight = -3.25f;

    // Update is called once per frame
    void Update()
    {
        if (!isUp)
        {
            transform.position += new Vector3(0f, step, 0f);
        }
        else 
        {
            transform.position -= new Vector3(0f, step, 0f);
        }

        if (transform.position.y >= maxHeight)
        {
            isUp = true;
        }
        else if (transform.position.y <= minHeight)
        {
            isUp = false;
        }
    }
}
