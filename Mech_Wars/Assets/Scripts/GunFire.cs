using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour {

    float gunTimer;
    public float gunCooldown;
    public Transform FirePoint;
    RaycastHit rayHit;
    public GameObject BulletHole;
    public GameObject MuzzleFlash;
    MuzzleFlash MuzzleFlashComponent;


    // Use this for initialization
    void Start()
    {
        MuzzleFlashComponent = MuzzleFlash.GetComponent<MuzzleFlash>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) {
            if (Time.time > gunTimer)
            {
                if(Physics.Raycast(FirePoint.position,FirePoint.right, out rayHit))
                {
                    gunTimer = Time.time + gunCooldown;
                    Vector3 BulletHolePos = rayHit.point + rayHit.normal * 0.01f;
                    Quaternion BulletHoleRot = Quaternion.FromToRotation(-Vector3.forward, rayHit.normal);
                    GameObject obj = Instantiate(BulletHole,BulletHolePos,BulletHoleRot) as GameObject;


                    StartCoroutine(MuzzleFlashComponent.Muzzle());



                }
            
            }
        }
    }
}
