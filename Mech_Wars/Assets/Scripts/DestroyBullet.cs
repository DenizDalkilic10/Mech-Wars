using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    public GameObject particle;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject temp = Instantiate(particle, this.transform.position, Quaternion.identity);
        Destroy(temp, 1);
        Destroy(gameObject);
    }
}
