using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engineFlameController : MonoBehaviour {

    private ParticleSystem ps;
    public bool moduleEnabled = false;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = false;
    }

    void Update()
    {
        var emission = ps.emission;
        if (Input.GetButton("Vertical"))
        {
            emission.enabled = true;
        }
        else if (Input.GetButtonUp("Vertical"))
            emission.enabled = false;
    }
}
