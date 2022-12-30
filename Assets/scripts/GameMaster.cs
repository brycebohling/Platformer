using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private Transform player;
    public int whichRP = 0;
    private string checkPoint;
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] private float respawnCountdown;
    private Vector3 playerRespawnPoint;
    public void killPlayer()
    {   
        // death particales
        deathEffect.gameObject.SetActive(true);
        deathEffect.Stop();
        deathEffect.transform.position = new Vector2(player.position.x, player.position.y);
        deathEffect.Play();
        // disabling the player
        player.gameObject.SetActive(false);
        // finding where the respawn point is and adding +1 to the y
        checkPoint = "checkPoint" + whichRP;

        Vector3 checkPointPosition = GameObject.FindGameObjectWithTag(checkPoint).transform.position;

        playerRespawnPoint = new Vector3(checkPointPosition.x, checkPointPosition.y + 1, transform.position.z);

        Invoke("respawnPlayer", respawnCountdown);
    }
    private void respawnPlayer()
    {   
        // seting the player position to the respawn point 
        player.position = playerRespawnPoint;
        player.gameObject.SetActive(true);
    }
}
