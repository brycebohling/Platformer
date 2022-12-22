using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform Player;

    void Update()
    {
        transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
    }
}
