using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunRotation : MonoBehaviour {

    // Use this for initialization
    public GameObject cameraPivot;
    Quaternion cameraPivotCopy;

	void Start ()
    {
        cameraPivotCopy = new Quaternion(); 
	}
	
	// Update is called once per frame
	void Update ()
    {
        cameraPivotCopy = cameraPivot.transform.rotation;
        gameObject.transform.rotation = cameraPivotCopy;
	}
}
