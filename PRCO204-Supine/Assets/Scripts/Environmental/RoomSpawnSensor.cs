﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnSensor : MonoBehaviour
{

    // Each room spawn point has 4 sensors.
    // These identify any rooms surrounding the spawn point, to dictate what requirements
    // the new room should meet to avoid doors to nowhere.

    // VARS
    public string direction;
    private BoxCollider sensorCollider;
    private Room foundRoom;
    [SerializeField] private bool mustHaveDoor = false;
    [SerializeField] private bool mustNotHaveDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        // collider is off, toggled on to check relevant surroundings
        sensorCollider = gameObject.GetComponent<BoxCollider>();
        //sensorCollider.enabled = false;

        //checkSensor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkMustHave()
    {
        //toggleSensorCollider();
        return mustHaveDoor;
    }

    public bool checkMustNot()
    {
        //toggleSensorCollider();
        return mustNotHaveDoor;
    }

    public string getRequiredDoorDir()
    {
        return direction;
    }

    public bool checkRoomWasFound()
    {
        if (foundRoom)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //public void checkSensor()
    //{
    //    StartCoroutine("toggleSensorCollider");
    //}

    //public IEnumerator toggleSensorCollider()
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    sensorCollider.enabled = true;
    //    yield return new WaitForSeconds(0.5f);
        
    //}

    private void OnTriggerStay(Collider other)
    {
        // when something alerts the sensor, it checks the associated room and 
        // updates the 'need related room' bool

        if (foundRoom == null)
        {
            foundRoom = other.gameObject.GetComponentInParent<Room>();

            if (other.gameObject.GetComponentInParent<Room>())
            {

                if (direction == "N")
                {
                    if (foundRoom.sDoor)
                    {
                        mustHaveDoor = true;
                        mustNotHaveDoor = false;
                    }
                    else
                    {
                        mustNotHaveDoor = true;
                        mustHaveDoor = false;
                    }
                }
                if (direction == "E")
                {
                    if (foundRoom.wDoor)
                    {
                        mustHaveDoor = true;
                        mustNotHaveDoor = false;
                    }
                    else
                    {
                        mustNotHaveDoor = true;
                        mustHaveDoor = false;
                    }
                }
                if (direction == "S")
                {
                    if (foundRoom.nDoor)
                    {
                        mustHaveDoor = true;
                        mustNotHaveDoor = false;
                    }

                    else
                    {
                        mustNotHaveDoor = true;
                        mustHaveDoor = false;
                    }
                }
                if (direction == "W")
                {
                    if (foundRoom.eDoor)
                    {
                        mustHaveDoor = true;
                        mustNotHaveDoor = false;
                    }

                    else
                    {
                        mustNotHaveDoor = true;
                        mustHaveDoor = false;
                    }
                }
                //sensorCollider.enabled = false;
            }
        }
    }

        
}