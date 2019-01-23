using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    //VARIABLES

    //enum
    public enum AxeState { Static, Thrown, Travelling, Returning, Returned }
    public AxeState axeState;

    //GameObject
    public GameObject axe;
    public GameObject axeTempHolder;

    //float
    public float axeFlightSpeed;
    public float axeThrowPower;
    public float axeRotationSpeed;
    private float startTime;
    private float journeyLength;

    //int
    public int playerNumber;
    public Rigidbody axeRb;
   
    //string
    private string shootingController;
    
    //script
    public PlayerMovement playerScript;
    public NewAxeCollisions axeCollisions;
    public Shooting sh;

    //Vector3
    private Vector3 tempScale;
    private Vector3 startPos;
    private Vector3 endPos;

    //bool
    private bool onAir = false;
    
    //METHODS


    // Use this for initialization
    void Start()
    {
        playerNumber = playerScript.playerNum; //if crashes put it into update method
        shootingController = "Vertical"; //just for initialization changes in update
        axeRb = axe.GetComponent<Rigidbody>();
        axeRb.isKinematic = true;
        axeRb.useGravity = false;
        axeState = AxeState.Static;
        axe.transform.parent = axeTempHolder.transform;
    }

    void FixedUpdate()
    {
        playerNumber = playerScript.playerNum;

        if(playerScript.playerNum > 0)
            shootingController = CharacterSelection.throwableXbox[playerScript.playerNum - 1];
       
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

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
        {
            sh.Targettransform = hit.point;
        }
    }

    void ThrownAxeWithPhysics()
    { 
        axe.transform.parent = null;
        axeState = AxeState.Travelling;
        axeRb.isKinematic = false;
        axeRb.useGravity = true;
        axe.transform.position = axeTempHolder.transform.position;
        axe.transform.rotation = axeTempHolder.transform.rotation;
        axeRb.AddForce(-axe.transform.right * axeThrowPower);
    }

   public  void RecallAxe()
    {
        axe.transform.parent = null;
        float distCovered = (Time.time - startTime) * axeFlightSpeed;
        float fracJourney = distCovered / journeyLength;
        axe.transform.position = Vector3.Lerp(startPos, endPos,fracJourney);
        axeCollisions.col.enabled = false;
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