using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    //VARIABLES

    //float
    public float lookRadius = 10f;
    private float targetDistance;
    private float eyeSightAngle;
    private float fieldOfViewAngle = 120;
    private float lastCalculatedTime;

    //int
    private int randomControlPoint;

    //GameObject
    public GameObject cameraPivot;
    public GameObject[] controlPoints = new GameObject[4];
    Transform targetTransform;
    [HideInInspector]
    public GameObject target;
    private GameObject[] enemyArray = new GameObject[4];

    //script
    public NavMeshAgent agent;
    public BotShooting botShooter;
    public PlayerMovement player;
    //bool
    private bool randomPathFound = false;
    private bool isInEyeSight = false;
    private bool IsnonTargetDestinationSet;
    private bool goingTowardsPowerUp;

    public bool GoingTowardsPowerUp
    {
        get
        {
            return goingTowardsPowerUp;
        }

        set
        {
            goingTowardsPowerUp = value;
        }
    }

    public bool IsnonTargetDestinationSet1
    {
        get
        {
            return IsnonTargetDestinationSet;
        }

        set
        {
            IsnonTargetDestinationSet = value;
        }
    }

    //METHODS


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyArray = GameObject.FindGameObjectsWithTag("Player");
        IsnonTargetDestinationSet1 = false;
        targetDistance = int.MaxValue;
        target = null;
        lastCalculatedTime = -4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        isInEyeSight = false;
        enemyArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject healthUP = GameObject.FindGameObjectWithTag("healthUP");
        GameObject staminaUP = GameObject.FindGameObjectWithTag("staminaUP");

        for (int i = 0; i < enemyArray.Length; i++)
        {

            if (enemyArray[i] && enemyArray[i] != this.gameObject) // added
            {
                float distance = Vector3.Distance(enemyArray[i].transform.position, transform.position);

                if (distance < targetDistance && distance != 0)
                {
                    target = enemyArray[i];
                    targetTransform = target.transform;
                }

                if (target)
                {
                    targetDistance = Vector3.Distance(target.transform.position, transform.position);
                    Vector3 vectorBetweenBotAndEnemy = (target.transform.position - transform.position);
                    eyeSightAngle = Vector3.Angle(vectorBetweenBotAndEnemy, transform.forward);
                }
                else
                {
                    targetDistance = int.MaxValue;
                }
            }

            if (targetDistance <= lookRadius && enemyArray[i] != this.gameObject && eyeSightAngle < fieldOfViewAngle * 0.5f) // add eyesight
            {
                isInEyeSight = true;
                IsnonTargetDestinationSet = false;

                if (targetTransform)
                {
                    agent.SetDestination(targetTransform.position);  //object destroyed but still trying to access it
                    agent.autoRepath = true;

                    if (targetDistance <= agent.stoppingDistance)
                    {
                        FaceTarget(targetTransform);
                    }
                    if (targetDistance <= 700) //shotDistance variable will be added
                    {
                        if (Time.time - botShooter.timeLastShot > botShooter.shotFrequency)
                        {
                            cameraPivot.transform.LookAt(targetTransform);
                            botShooter.BotFire();
                        }
                        else
                        {
                            cameraPivot.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                        }    
                    }
                }
            }
        } //finds the target at the end of this scope

        if (!isInEyeSight && !IsnonTargetDestinationSet)
        {
            if (player.health <= 50 && healthUP != null)
            {
                agent.SetDestination(healthUP.transform.position);
                agent.stoppingDistance = 0;
                Debug.Log("Executed health");
                IsnonTargetDestinationSet = true; // edited 
                goingTowardsPowerUp = true;
            }
            else if (player.stamina <= 40 && staminaUP != null)
            {
                Debug.Log("Executed stamina");
                agent.SetDestination(staminaUP.transform.position);
                agent.stoppingDistance = 0;
                IsnonTargetDestinationSet = true;  //edited
                goingTowardsPowerUp = true;
            }
            else
            {
                randomControlPoint = Random.Range(0, 4);
                agent.stoppingDistance = 0;
                // Debug.Log("Random Control Point = " + randomControlPoint);
                agent.SetDestination(controlPoints[randomControlPoint].transform.position);
                IsnonTargetDestinationSet = true;
                goingTowardsPowerUp = false;
            }
        }

        if (((player.health <= 50 && healthUP) || (player.stamina <= 40 && staminaUP)) && !goingTowardsPowerUp)
            IsnonTargetDestinationSet = false;

        if (!isInEyeSight && IsnonTargetDestinationSet && gameObject.transform.position.x == controlPoints[randomControlPoint].transform.position.x && gameObject.transform.position.z == controlPoints[randomControlPoint].transform.position.z)
        {
            int temp = randomControlPoint;
            while (temp == randomControlPoint)
            {
                randomControlPoint = Random.Range(0, 4);
            }
            // Debug.Log("New Control Point = " + randomControlPoint);
            agent.SetDestination(controlPoints[randomControlPoint].transform.position);
        }
        //Debug.Log(isInEyeSight + " --- " + IsnonTargetDestinationSet);



    }

    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void randomDestination()
    {

        if (!isInEyeSight && IsnonTargetDestinationSet)
        {

            int temp = randomControlPoint;
            //Vector3 axis = controlPoints[temp].transform.position - transform.position;
            //axis = transform.position - axis;

            while (temp == randomControlPoint)
            {
                randomControlPoint = Random.Range(0, 4);
            }

            // Debug.Log("New Control Point = " + randomControlPoint);
            if (Time.time - lastCalculatedTime > 2f)
            {

                agent.SetDestination(transform.position - (agent.destination - transform.position));
              
                //agent.SetDestination(new Vector3(-this.transform.position.x, this.transform.position.y, -this.transform.position.z));
                Debug.Log("Taken Hit Behind The Back");
                //Debug.Log(-agent.destination);
                lastCalculatedTime = Time.time;
                IsnonTargetDestinationSet1 = true;
            }


        }
    }
}