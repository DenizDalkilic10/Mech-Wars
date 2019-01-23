using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public float moveSpeed = 110.0f;
    private int rotZ = 270;
    
    void Start()
    {
        transform.Rotate(Vector3.left, rotZ);
    }
    
    void Update()
    { 
        transform.Translate( Vector3.up * moveSpeed * Time.deltaTime);
        Destroy(gameObject, 7.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); //edited
    }
}

