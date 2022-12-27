using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bullet;
    private float timebetween;
    public float starttimebetween;


    void Start()
    {
        timebetween = starttimebetween;
    }



    void Update()
    {
        if(timebetween <= 0)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            timebetween = starttimebetween;
        } else 
        {
            timebetween -= Time.deltaTime;
        }
    }
}
