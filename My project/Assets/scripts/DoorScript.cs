using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class DoorScript : MonoBehaviour
    {
        public void OpenTempleDoor()
        {
            //once player collects all gold ingots, the temple door will open
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }


