using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCam : MonoBehaviour {

    private string DeathCamController;
    Camera cam;
    GameObject[] Players;
    int index;
    float timer;
    public Text spc;


    public string DeathCamController1
    {
        get
        {
            return DeathCamController;
        }

        set
        {
            DeathCamController = value;
        }
    }

    void Start () {
        timer = 0;
        index = 0;
        cam = this.transform.GetChild(0).GetComponent<Camera>();

    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Players = GameObject.FindGameObjectsWithTag("Bot");
        if (Input.GetButtonDown(DeathCamController))
        {
            index++;
            if (index >= Players.Length) index = 0;            
        }
        if (timer > 5)
        {
            spc.text = "You are watching " + Players[index].name;
            try
            {
                if (Players[index].name == "gepardFinal(Clone)" || Players[index].name == "rhinoFinal(Clone)")
                {
                    cam.transform.position = Players[index].transform.GetChild(0).transform.GetChild(0).GetComponent<Camera>().transform.position;
                    cam.transform.rotation = Players[index].transform.GetChild(0).transform.GetChild(0).GetComponent<Camera>().transform.rotation;
                    cam.farClipPlane = 5000;
                }
                else
                {
                    cam.transform.position = Players[index].transform.GetChild(0).GetComponent<Camera>().transform.position;
                    cam.transform.rotation = Players[index].transform.GetChild(0).GetComponent<Camera>().transform.rotation;
                    cam.farClipPlane = 5000;
                }
            }
            catch
            {
                
            }
        }
    }
}
