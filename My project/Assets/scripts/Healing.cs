using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    private List<GameObject> InteractableObjList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //adds all the interactable objects into this list
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Interactable")) 
        {
 
             InteractableObjList.Add(obj);
        }
        SpawnHeal();
    }


    //spawns the painkillers in the location they were disabled
    private void SpawnHeal()
    {
        foreach(GameObject obj in InteractableObjList)
        {
            if(!obj.activeSelf)
            {
                obj.SetActive(true);
            }
        }
    }
}
