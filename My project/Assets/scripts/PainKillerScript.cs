using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableObjects
{
    public class PainKillerScript : MonoBehaviour,IInteractable
    {
        [SerializeField]
        private Player player;
        public void Use()
        {
            //if player health is full, the full health message will be displayed
            if (player.health >= player.max_health)
            {
                player.displayFullHealthText = true;
            }
            else
            {
                //the heal will be used and disabled. The player health will be updated
                if(gameObject.activeSelf == true)
                {
                    player.HealPlayer();
                    gameObject.SetActive(false);
                }
            }
            
        }

        public void Collect()
        {
            //allows player to stack the painkillers for later use
            if(gameObject.activeSelf == true)
            {
                player.PainKillerText(1);
                gameObject.SetActive(false);
            }
            
        }
    }
}

