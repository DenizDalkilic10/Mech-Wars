using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAxeCollisions : MonoBehaviour
{
    public Rigidbody rb;
    public Axe axe;
    public BoxCollider col;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        axe.CollisionOccured();
        rb.useGravity = false;
        rb.isKinematic = true;
        AddConstraints();
        col.enabled = false;
    }

    public void AddConstraints()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    public void RemoveConstraints()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

}