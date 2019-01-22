using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionRotation : MonoBehaviour {
    //variables
    private Rigidbody rb;
    private float rotSpeed;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rotSpeed = 0.8f;
	}
	
	// Update is called once per frame
	void Update () {
        if(!(SceneManager.GetActiveScene().name == "BattleScene"))
            transform.Rotate(new Vector3(0,rotSpeed,0),Space.World);
	}
}
