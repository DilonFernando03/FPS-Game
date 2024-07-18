using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeHealScript : MonoBehaviour
{
    [SerializeField]
    private Player player;
    private float dist;
    private float minDist = 4f;
    public bool playerInDome = false;

    void Update()
    {
        //To get the distance of the player from the dome
        dist = Vector3.Distance(player.transform.position, transform.position);

        //if player in range
        if (dist < minDist)
        {
            //if player health is not max, then heal the player
            if (player.health != player.max_health)
            {
                //to disable all shooting
                playerInDome = true;

                player.HealPlayer();
            }
        }
        else
        {
            //shooting will be stay/be enabled
            playerInDome = false;
        }
    }
}
