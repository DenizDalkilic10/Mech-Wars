using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAxeCollisions : MonoBehaviour
{
    public Rigidbody rb;
    public Axe axe;
    public BoxCollider col;
    private GameObject collidedObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        collidedObject = null;
    }

    void Update()
    {
        if (collidedObject != null && (collidedObject.GetComponent<PlayerMovement>() != null))
        {
            if (collidedObject.GetComponent<PlayerMovement>().health <= 5)
            {
                this.transform.parent = axe.axeTempHolder.transform;
                if (axe.axeState != Axe.AxeState.Static)
                    axe.RecallAxe();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        axe.CollisionOccured();
        rb.useGravity = false;
        rb.isKinematic = true;
        AddConstraints();
        col.enabled = false;
        collidedObject = collision.gameObject;
        if (collidedObject != null && (collidedObject.GetComponent<PlayerMovement>() != null))
        {

            if (!(collision.gameObject.GetComponent<PlayerMovement>().health <= 0))
                this.transform.parent = collision.gameObject.transform;
        }
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