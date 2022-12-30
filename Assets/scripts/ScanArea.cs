using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanArea : MonoBehaviour
{
    GameMaster gm;
    [SerializeField] private Transform player;    
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserSpeed;
    private Quaternion originalRotation;
    private Quaternion minRotation;
    private Quaternion maxRotation;
    [SerializeField] private float timeToKill;
    private float timeToKillCountdown;
    private bool onPlayer;
    private float timer;
    private float followPower = 100f;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();

        timeToKillCountdown = timeToKill;
        originalRotation = transform.rotation;

        minRotation = originalRotation * Quaternion.Euler(0, 0, -140);
        maxRotation = originalRotation * Quaternion.Euler(0, 0, -220);
    }

    void Update()
    {
        // Note that Vector3 is a "struct" -> there is no need to manually use "new Vector3(transform.position.x, ...)"
        var startPosition = transform.position;

        lineRenderer.SetPosition(0, startPosition);
        
        var factor = Mathf.PingPong(timer * laserSpeed, 1);
        timer += Time.deltaTime;
        // instead of the eulers rather use Quaternion
        if (!onPlayer)
        {
            transform.rotation = Quaternion.Lerp(minRotation, maxRotation, factor);
            
        }
        
        // "transform.up" basically equals using "transform.TransformDirection(Vector3.up)"
        var raycastHit2D = Physics2D.Raycast(startPosition, transform.up, 10f);

        if(raycastHit2D.collider)
        {
            // when you hit something actually use this hit position as the end point for the line
            lineRenderer.SetPosition(1, raycastHit2D.point);

            if (raycastHit2D.collider.gameObject.CompareTag("Player"))
            {
                onPlayer = true;
                Vector3 direction = player.position - transform.position;
                // finding the  angle using invrse tan (have to -90 everytime because unity thing is always 90 off)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                // put the angle found into a axis, in this case it's the z axis
                Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, followPower * Time.deltaTime);
                // transform.eulerAngles = Vector3.forward * angle;
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;

                timeToKillCountdown -= Time.deltaTime;
                if (timeToKillCountdown < 0)
                {
                    gm.killPlayer();
                }
            }
            else 
            {
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                timeToKillCountdown = timeToKill;
                onPlayer = false;
            }
        }
        else
        {
            // otherwise from the start position go 10 units in the up direction of your rotated object
            lineRenderer.SetPosition(1, startPosition + transform.up * 10f);
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
            timeToKillCountdown = timeToKill;
            onPlayer = false;
        }  
    }
}   
