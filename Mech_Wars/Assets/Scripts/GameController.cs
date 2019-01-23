using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{

    //VARIABLES

    //GameObject
    public GameObject[] CharacterArray = new GameObject[2];  // decrease the size to 2 and use the same prefabs for each player
    public GameObject[] BotArray = new GameObject[2];
    public static GameObject[] playerArray = new GameObject[4];
    public GameObject staminaPU, healthPU, ammoPU, portal;

    //float
    public static float[] health = new float[4];
    public static float[] stamina = new float[4];
    private float[] tempHealthArray = new float[4];
    private float[] tempStaminaArray = new float[4];

    //RawImage
    public RawImage[] playerHealthArray = new RawImage[4];
    public RawImage[] playerStaminaArray = new RawImage[4];

    //PlayerMovement
    [HideInInspector]
    public PlayerMovement[] playerScriptArray = new PlayerMovement[4];

    //Canvas
    public Canvas[] canvasArray = new Canvas[4];

    //////////////////////
    private Vector3 staminaPowerUpPos, healthPowerUpPos, portalPos, ammoPowerUpPos;
    public static float timeLastStaminaPU, timeLastHealthPU, timeLastAmmo;
    float staminaPUFrequency = 10.0f;
    float healthPUFrequency = 10.0f;
    float ammoPUFrequency = 10.0f;
    public static bool ammoPuPresent = false;
    public static bool healthPuPresent = false;
    public static bool staminaPuPresent = false;

    void Start()
    {
        initializeScene();

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            playerHealthArray[i].transform.localScale = new Vector3(health[i] / tempHealthArray[i], 1, 1);
            playerStaminaArray[i].transform.localScale = new Vector3(stamina[i] / tempStaminaArray[i], 1, 1);
        }
        //if (Time.time - timeLastAmmo > ammoPUFrequency && !ammoPuPresent)
        //    formAmmoPU();
        if (Time.time - timeLastHealthPU > healthPUFrequency && !healthPuPresent)
            formHealthPU();
        if (Time.time - timeLastStaminaPU > staminaPUFrequency && !staminaPuPresent)
            formStaminaPU();
    }

    void initializeScene()
    {

        CharacterSelection.sceneChanged = true;

        for (int i = 0; i < 4; i++)
        {
            if (CharacterSelection.characterChoices[i] == 1)  //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                playerArray[i] = Instantiate(CharacterArray[0], GameObject.Find("controlPoint" + (i + 1)).transform.position, Quaternion.identity) as GameObject;
            }
            if (CharacterSelection.characterChoices[i] == 2)
            {
                playerArray[i] = Instantiate(CharacterArray[1], GameObject.Find("controlPoint" + (i + 1)).transform.position, Quaternion.identity) as GameObject;
            }
            else if (CharacterSelection.characterChoices[i] == 0)
            {
                playerArray[i] = Instantiate(BotArray[Random.Range(0, 1)], GameObject.Find("controlPoint" + (i + 1)).transform.position, Quaternion.identity) as GameObject;
                canvasArray[i].enabled = false;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            playerScriptArray[i] = playerArray[i].GetComponent<PlayerMovement>();

        }

        for (int i = 0; i < 4; i++)
        {
            if (canvasArray[i].enabled)
                canvasArray[i].GetComponent<Canvas>().worldCamera = playerArray[i].transform.Find("CameraPivot").transform.Find("Camera").gameObject.GetComponent<Camera>();
        }

        for (int i = 0; i < 4; i++)
        {
            if (playerScriptArray[i])
                tempHealthArray[i] = playerScriptArray[i].health;
            else //if bot
                tempHealthArray[i] = 100;
        }

        for (int i = 0; i < 4; i++)
        {
            if (playerScriptArray[i])
                tempStaminaArray[i] = playerScriptArray[i].stamina;
            else //if bot
                tempStaminaArray[i] = 100;
        }
    }

    void formStaminaPU()
    {

        staminaPowerUpPos.y = 20;
        staminaPowerUpPos.x = Random.Range(500, 1500);
        staminaPowerUpPos.z = Random.Range(500, 1500);
        Instantiate(staminaPU, staminaPowerUpPos, Quaternion.identity);
        staminaPuPresent = true;
        //timeLastStaminaPU= Time.time;
    }
    void formHealthPU()
    {

        healthPowerUpPos.y = 20;
        healthPowerUpPos.x = Random.Range(500, 1500);
        healthPowerUpPos.z = Random.Range(500, 1500);
        Instantiate(healthPU, healthPowerUpPos, Quaternion.identity);
        healthPuPresent = true;
        //timeLastHealthPU = Time.time;
    }
    void formAmmoPU()
    {

        ammoPowerUpPos.y = 20;
        ammoPowerUpPos.x = Random.Range(500, 1500);
        ammoPowerUpPos.z = Random.Range(500, 1500);
        Instantiate(ammoPU, ammoPowerUpPos, Quaternion.identity);
        //timeLastAmmo = Time.time;
    }
}