using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{ 
    float bulletSpeed = 20000;
    public GameObject bullet;
    public GameObject MuzzleFlash;
   // Quaternion rot;
    public static int characterNum = 1;
    private string fire;
   

    // Use this for initialization
    void Start()
    {
        fire = CharacterSelection.fireXbox[characterNum-1];
        characterNum++;
    }

    void Fire()
    {
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation);
        GameObject tempMuzzle = Instantiate(MuzzleFlash, transform.position, MuzzleFlash.transform.rotation); 
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.right * bulletSpeed);
        Destroy(tempBullet, 7.0f); 
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(fire))
        {
            Fire();
        }
    }
}