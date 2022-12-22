using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeScript : MonoBehaviour
{
    [SerializeField] private Transform Player;
    public int whichCP = 0;
    private string checkPoint;



    public void respawn()
    {
        if (whichCP > 0)
        {
            checkPoint = "checkPoint" + whichCP;

            Vector3 checkPointPosition = GameObject.FindGameObjectWithTag(checkPoint).transform.position;

            Player.position = new Vector3(checkPointPosition.x, checkPointPosition.y + 1, Player.position.z);
        } else
        {
            Player.position = new Vector3(0, -1, Player.position.z);
        }
    }
}
