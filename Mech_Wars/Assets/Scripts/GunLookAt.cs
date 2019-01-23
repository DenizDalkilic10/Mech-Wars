using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLookAt : MonoBehaviour {

    Camera cam;
    public GameObject gun;

	// Use this for initialization
	void Start () {
        cam = this.gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        //Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5F, 0));
        Ray ray = new Ray(cam.transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            Debug.Log(hit.transform.name);
            gun.transform.LookAt(hit.point);
    }
}
