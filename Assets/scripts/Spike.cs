using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    PlayerLifeScript PLS;
    

    private void Start()
    {
        PLS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLifeScript>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PLS.respawn();
        }
    }
}
        