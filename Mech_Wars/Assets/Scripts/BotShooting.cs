using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShooting : MonoBehaviour
{
    float bulletSpeed = 20000;
    public GameObject bullet;
    public GameObject MuzzleFlash;
    public float shotFrequency = 0.4f;
    public float timeLastShot;
  
   

    public void BotFire()
    {
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation);
        GameObject tempMuzzle = Instantiate(MuzzleFlash, transform.position, MuzzleFlash.transform.rotation);
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 7.0f);
        timeLastShot = Time.time;
    }
}