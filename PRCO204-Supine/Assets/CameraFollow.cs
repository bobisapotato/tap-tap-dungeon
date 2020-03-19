﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothTime;
    private Vector3 velocity = Vector3.zero;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z); //playergame.GetComponent<Raycast>().raycam.transform.position.y
        Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(origin, targetPosition, ref velocity, smoothTime);
    }
}
