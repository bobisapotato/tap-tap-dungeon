﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameObject[] pathPositions;

    private Rigidbody rb;

    private int currentPos = 0;

    private float wanderingSpeed = 5f;
    private float followingSpeed = 6.66f;
    private float rotationSpeed = 5f;
    private float radius = 5f;
    private float step;

    private bool isFollowing;
    private bool isMoving = true;
    private bool isRotating = false;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the distance to the player.
        float movementDistance = Vector3.Distance(target.position, transform.position);

        // If inside the radius.
        if (movementDistance <= radius)
        {
            step = followingSpeed * Time.deltaTime;

            StartCoroutine(FaceTarget(target.gameObject));

            if (!isRotating)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }

            isFollowing = true;
        }
        else
        {
            step = wanderingSpeed * Time.deltaTime;

            if (isFollowing)
            {
                StartCoroutine(PauseMovement());

                isFollowing = false;
            }
            else
            {
                MoveTowardsNextPoint();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == pathPositions[currentPos])
        {
            int oldPos = currentPos;

            currentPos = Random.Range(0, pathPositions.Length);

            if (currentPos == oldPos)
            {
                currentPos++;

                if (currentPos >= pathPositions.Length)
                {
                    currentPos = 0;
                }
            }
        }
    }

    void MoveTowardsNextPoint()
    {
        if (isMoving)
        {
            StartCoroutine(FaceTarget(pathPositions[currentPos]));

            if (!isRotating)
            {
                rb.position = Vector3.MoveTowards(rb.position, pathPositions[currentPos].transform.position, step);
            }
            isFollowing = false;
        }
    }

    // Point towards the target GameObject.
    IEnumerator FaceTarget(GameObject target)
    {
        // Wait for a bit.
        yield return new WaitForSeconds(0.2f);

        isRotating = true;

        Vector3 direction = (target.transform.position - rb.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation
            (new Vector3(direction.x, 0, direction.z));
        rb.rotation = Quaternion.Slerp(rb.rotation,
            lookRotation, Time.deltaTime * rotationSpeed);

        if (rb.rotation == lookRotation) 
        {
            isRotating = false;
        }
    }

    IEnumerator PauseMovement()
    {
        // Backup and clear velocities.
        Vector3 linearBackup = rb.velocity;
        Vector3 angularBackup = rb.angularVelocity;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Finally freeze the body in place so forces like gravity or movement won't affect it.
        rb.constraints = RigidbodyConstraints.FreezeAll;

        isMoving = false;

        // Wait for a bit.
        yield return new WaitForSeconds(1);

        // Unfreeze before restoring velocities.
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Restore the velocities.
        rb.velocity = linearBackup;
        rb.angularVelocity = angularBackup;

        isMoving = true;
    }
}
