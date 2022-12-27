using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    PlayerLifeScript PLS;

    private bool cantRun = false;
    private Animator anim;
     
    private void Start()
    {
        PLS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLifeScript>();
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!cantRun && collider.gameObject.CompareTag("Player"))
        {
                PLS.whichCP++;
                cantRun = true;
                anim.SetBool("collided", true);
        }
    }
}
