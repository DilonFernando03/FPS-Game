using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    public SensitivityScript sensitivityScript;
    float xRotation = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float CalculatedMouseSens = (sensitivityScript.GetSens() * 50);
        float Xmove = Input.GetAxis("Mouse X") * CalculatedMouseSens * Time.deltaTime;
        float Ymove = Input.GetAxis("Mouse Y") * CalculatedMouseSens * Time.deltaTime;
        xRotation -= Ymove;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //rotate camera and orientation
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        player.Rotate(Vector3.up * Xmove);
        if (MenuManager.isGamePaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        
    }
}
