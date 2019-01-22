using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    public GameObject axe;
    public Rigidbody axeRb;
    public GameObject axeTempHolder;
    public int playerNumber;
    private string shootingController;
    public float axeFlightSpeed = 200f;
    public float axeThrowPower = 40000f;
    public float axeRotationSpeed = 200f;
    public PlayerMovement playerScript;
    public NewAxeCollisions axeCollisions;

    //instead of using multiple booleans use an enum
    public enum AxeState { Static, Thrown, Travelling, Returning, Returned }
    public AxeState axeState;

    private float startTime;
    private float journeyLength;

    private Vector3 startPos;
    private Vector3 endPos;
    bool onAir = false;
    

    // Use this for initialization
    void Start()
    {
        playerNumber = playerScript.playerNum; //if crashes put it into update method
        shootingController = "Jump"; //just for initialization changes in update
        axeRb = axe.GetComponent<Rigidbody>();
        axeRb.isKinematic = true;
        axeRb.useGravity = false;
        axeState = AxeState.Static;
    }

    void FixedUpdate()
    {
        playerNumber = playerScript.playerNum;
        shootingController = CharacterSelection.throwableXbox[playerScript.playerNum - 1];
        //axe.transform.localScale = new Vector3(1,1,1); //take the first scale as temp and apply it in every update call to prevent change in size 
       
        if (Input.GetButtonDown(shootingController) && !onAir)
        {
            axeState = AxeState.Thrown;
            onAir = true;
            axe.transform.parent = null;
            
        }
        else if (Input.GetButtonDown(shootingController) && onAir )
        {
            startPos = axe.transform.position;
            endPos = axeTempHolder.transform.position;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPos, endPos);
            axeState = AxeState.Returning;
        }

        if (axeState == AxeState.Thrown)
        {
            ThrownAxeWithPhysics();
        }

        if (axeState == AxeState.Travelling || axeState == AxeState.Returning)
        {
            axe.transform.Rotate(0, 0, 2.0f * axeRotationSpeed * Time.deltaTime);
        }

        if (axeState == AxeState.Returning)
        {
            RecallAxe();
        }
    }

    void ThrownAxeWithPhysics()
    {
       
        axe.transform.parent = null;
        axeState = AxeState.Travelling;
        axeRb.isKinematic = false;
        
        axe.transform.position = axeTempHolder.transform.position;
        axe.transform.rotation = axeTempHolder.transform.rotation;
       
        axeRb.AddForce(-axe.transform.right * axeThrowPower);
    }

    void RecallAxe()
    {
        float distCovered = (Time.time - startTime) * axeFlightSpeed;
        float fracJourney = distCovered / journeyLength;
        axe.transform.position = Vector3.Lerp(startPos, endPos,fracJourney);
        if (fracJourney >= 1.0f)
        {
            RecalledAxe();
        }
    }

    void RecalledAxe()
    {
        axeState = AxeState.Static;
        axeCollisions.RemoveConstraints();
        axe.transform.position = axeTempHolder.transform.position;
        axe.transform.rotation = axeTempHolder.transform.rotation;
        axeRb.isKinematic = true;
        axeRb.useGravity = false;
        axe.transform.parent =axeTempHolder.transform;
        onAir = false;
        axeCollisions.col.enabled = true;
    }

    public void CollisionOccured()
    {
        axeState = AxeState.Static;
    }

}