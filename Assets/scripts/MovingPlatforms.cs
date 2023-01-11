using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Transform collisionToughChecker;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;
    private bool toughingPlayer;
    
         
    void Update()
    {
        toughingPlayer = Physics2D.OverlapBox(collisionToughChecker.position, new Vector2(1f, .3f), 0, playerLayer);

        if (toughingPlayer)
        {
            player.transform.SetParent(transform);
        } else 
        {
            player.transform.SetParent(null);
        }
        
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp (pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
