using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanArea : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserSpeed;
    private Quaternion originalRotation;
    private Quaternion minRotation;
    private Quaternion maxRotation;


    void Start()
    {
        originalRotation = transform.rotation;

        minRotation = originalRotation * Quaternion.Euler(0, 0, -100);
        maxRotation = originalRotation * Quaternion.Euler(0, 0, -260);
    }

    void Update()
    {
        // Note that Vector3 is a "struct" -> there is no need to manually use "new Vector3(transform.position.x, ...)"
        var startPosition = transform.position;

        lineRenderer.SetPosition(0, startPosition);
        
        var factor = Mathf.PingPong(Time.time * laserSpeed, 1);

        // instead of the eulers rather use Quaternion
        transform.rotation = Quaternion.Lerp(minRotation, maxRotation, factor);

        // "transform.up" basically equals using "transform.TransformDirection(Vector3.up)"
        var raycastHit2D = Physics2D.Raycast(startPosition, transform.up, 10f);

        if(raycastHit2D.collider)
        {
            // when you hit something actually use this hit position as the end point for the line
            lineRenderer.SetPosition(1, raycastHit2D.point);

            if (raycastHit2D.collider.gameObject.CompareTag("Player"))
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }
            else 
            {
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
            }
        }
        else
        {
            // otherwise from the start position go 10 units in the up direction of your rotated object
            lineRenderer.SetPosition(1, startPosition + transform.up * 10f);
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }  
    }
}
