using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeScript : MonoBehaviour
{
    [SerializeField] private Transform Player;
    public int whichCP = 0;
    private string checkPoint;
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] private float respawnCountdown;


    public void respawn()
    {
        StartCoroutine("respawnCoroutine");
    }
    private IEnumerator respawnCoroutine()
    {
        gameObject.SetActive(false);

        deathEffect.gameObject.SetActive(true);
        deathEffect.Stop();
        deathEffect.transform.position = new Vector2(transform.position.x, transform.position.y);
        deathEffect.Play();

        checkPoint = "checkPoint" + whichCP;

        Vector3 checkPointPosition = GameObject.FindGameObjectWithTag(checkPoint).transform.position;

        Player.position = new Vector3(checkPointPosition.x, checkPointPosition.y + 1, Player.position.z);

        yield return new WaitForSeconds(respawnCountdown);
        Debug.Log("finaly");
        gameObject.SetActive(true);
        
    }
}
