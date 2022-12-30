using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    GameMaster gm;
    private bool cantRun = false;
    private Animator anim;
     
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!cantRun && collider.gameObject.CompareTag("Player"))
        {
                gm.whichRP++;
                cantRun = true;
                anim.SetBool("collided", true);
        }
    }
}
