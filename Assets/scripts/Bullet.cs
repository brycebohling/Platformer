using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerLifeScript PLS;
    public float speed;
    Rigidbody2D rb;
    void Start()
    {
        PLS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLifeScript>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.CompareTag("Player"))    
        {
            PLS.respawn();
        } 
        Destroy(gameObject);
    }
}
