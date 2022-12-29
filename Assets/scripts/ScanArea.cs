using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanArea : MonoBehaviour
{
//     [SerializeField] private float rotationSpeed;
//     [SerializeField] float visionDistance;
//     [SerializeField] LineRenderer lineOfSight;
//     private bool rayCastLeft;

//     void Start ()
//     {
        
//     }

//     void Update()
//     {
//         lineOfSight.SetPosition(0, transform.position);

//         RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, visionDistance);


//         if (transform.rotation.z > 0)
//         {
//             rayCastLeft = true;
//         }
//         else if (transform.rotation.z < -170)
//         {
//             rayCastLeft = false;
//         }

//         if (rayCastLeft)
//         {
//             transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
//             Debug.Log("hello");
//         } 
//         else
//         {
//             Debug.Log("hi");
//             transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
//         }


//         if (hitInfo.collider != null)
//         {
//             lineOfSight.SetPosition(1, hitInfo.point);
//             lineOfSight.startColor = Color.green;
//             lineOfSight.endColor = Color.green;
//             if (hitInfo.collider.tag == "Player")
//             {
//                 lineOfSight.startColor = Color.red;
//                 lineOfSight.endColor = Color.red;
//                 lockedOnPlayer();
//             }
//         }
//         else 
//         {
//             lineOfSight.SetPosition(1, transform.position + transform.right * visionDistance);
//             lineOfSight.startColor = Color.green;
//             lineOfSight.endColor = Color.green;
//         }
//     }
//     private void lockedOnPlayer()
//     {
        
//     }
    public LineRenderer lineRenderer;
    public LayerMask layerMask;
    public float laserSpeed;

    private Quaternion originalRotation;
    private Quaternion minRotation;
    private Quaternion maxRotation;


    void Start()
    {
        originalRotation = transform.rotation;

        minRotation = originalRotation * Quaternion.Euler(0, 0, -90);
        maxRotation = originalRotation * Quaternion.Euler(0, 0, -270);
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
        }  
    }
}
