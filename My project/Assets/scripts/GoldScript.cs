using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableObjects
{
    public class GoldScript : MonoBehaviour,IInteractable
    {
        [SerializeField]
        private Player player;

        public void Collect()
        {  
            //collect the gold ingot
            if (gameObject.activeSelf == true)
            {
                player.GoldText();
                gameObject.SetActive(false);
            }
            
        }

        public void Use()
        {
            return;
        }
    }
}
