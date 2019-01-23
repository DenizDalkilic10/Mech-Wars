using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{ 
    public float bulletSpeed = 10;
    public GameObject bullet;
    public GameObject MuzzleFlash;
    public float shotFrequency = 0.2f;
    public float timeLastShot;
    public static int characterNum = 1;
    private string fire;
    public Camera cam;
    private Vector3 targettransform;

    public Vector3 Targettransform
    {
        get
        {
            return targettransform;
        }

        set
        {
            targettransform = value;
        }
    }

    void Start()
    {
        fire = CharacterSelection.fireXbox[characterNum-1]; //find a better way to implement this
        characterNum++;
    }

    void Update()
    {
        this.transform.LookAt(Targettransform);

        if (Input.GetAxis(fire) != 0 && Time.time - timeLastShot > shotFrequency)
        {
            Fire();
        }
    }
    void Fire()
    {   
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation);
        GameObject tempMuzzle = Instantiate(MuzzleFlash, transform.position, MuzzleFlash.transform.rotation); 
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 7.0f);
        timeLastShot = Time.time;
    }
}