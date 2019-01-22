using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {
    public GameObject bullet;
    Vector3 bulletPos;
    public float shotFrequency = 0.5f;
    public float timeLastShot;
   
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Jump"))
        { 
                fire();
        } 
     }
       
    void fire()
    {
        if (Time.time - timeLastShot > 0.2)
        {
            //GameController.ammo--;
            bulletPos = transform.position;
            Instantiate(bullet, bulletPos, transform.rotation.normalized);
            timeLastShot = Time.time;
        }

    }
}
