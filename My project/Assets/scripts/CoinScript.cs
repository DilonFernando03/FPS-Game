using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableObjects
{   
    public class CoinScript : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private MenuManager menuManager;
        void Update()
        {
            //rotate the main coin in the temple (as an animation)
            transform.Rotate(0f, 0f, 50 * Time.deltaTime, Space.Self);
        }
        public void Use()
        {
            //can only be used once the player collects all gold ingots
            gameObject.SetActive(false);
            menuManager.DisplayGameOverUI();
        }

        public void Collect()
        {
            return;
        }

    }
}

