﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    private Collider hitbox;

    [SerializeField] GameObject bombRad;
    [SerializeField] int damage;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = bombRad.GetComponent<SphereCollider>();
        hitbox.enabled = false;
        StartCoroutine(DisableCollider());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(3.0f);

        hitbox.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("TakeDamage", damage);
            hitbox.enabled = false;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            HealthManager.playerHealth.TakeDamage(damage / 2);
            hitbox.enabled = false;
            Destroy(gameObject);
        }

        else
        {
            Invoke("Die", 3f);
            
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }

    
}