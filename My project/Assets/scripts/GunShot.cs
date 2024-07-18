using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject bulletPoint;
    [SerializeField]
    private float BulletSpeed = 5000f;

    private AudioSource bulletShotSound;

    //this method will be called from the AI and the player
    void Start()
    {
        bulletShotSound = GetComponent<AudioSource>();
    }
    public void Shoot()
    {
            if (MenuManager.isGamePaused == false)
            {
                //creating the bullet in the bullet spawn point attached to the gun
                GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);

                //release the bullet from that point
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed); 

                //play bullet sound effect
                bulletShotSound.Play();
                
                //destroys bullet after a second from launch
                Destroy(bullet,1);
            }
    }
}
