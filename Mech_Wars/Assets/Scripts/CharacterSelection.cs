using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    //variables

    //PlayerMovement
    private PlayerMovement[] PlayerSript = new PlayerMovement[4];

    //GameObjects
    [HideInInspector]
    public GameObject[] Bots;
    public GameObject[] PlayerStats = new GameObject[4];
    public GameObject[] PlayerCanvas = new GameObject[4];
    public GameObject[] characterArray = new GameObject[4]; // twice the times of characters
    private GameObject[] Players;
    private GameObject[] P;

    //int
    [HideInInspector]
    public static int JoysticCount = 0;
    private int BotCount, TempBot, TempPlayer = 0;
    private int j = 0;
    public static int[] playerControllerNumbers = new int[4];  //most xbox can handle  //controller numbers corresponding player indexes
    [HideInInspector]
    public static int[] characterChoices; //size of this array will be equal to maximum number of players

    //Text
    public Text[] PlayerText = new Text[4];
    public Text[] PlayerHealthText = new Text[4];
    public Text[] PlayerPowerText = new Text[4];
    public Text[] PlayerStaminaText = new Text[4];
    public Text Header;
    public Text ReadyText;

    //bool
    private bool[] PlayerReady = new bool[4];
    private bool[] PlayerCharChanges = new bool[4];
    private bool enabledchar = false;
    public static bool sceneChanged = false;

    // string ---- controllers for each button
    public static string[] submitXbox = new string[7];
    public static string[] horizontalXbox = new string[7];
    public static string[] verticalXbox = new string[7];
    public static string[] cameraXbox = new string[7];
    public static string[] fireXbox = new string[7];
    public static string[] throwableXbox = new string[7];
    public static string[] V_cameraXbox = new string[7];
    public static string[] StaminaXbox = new string[7];
    public static string[] DeathCamXbox = new string[7];
    public static string[] Zoomxbox = new string[7];

    // Use this for initialization
    void Start()
    {
        JoysticCount = Input.GetJoystickNames().Length;
        BotCount = 4 - JoysticCount;
        TempBot = BotCount;
        TempPlayer = JoysticCount;
        P = new GameObject[JoysticCount];
        Players = new GameObject[JoysticCount];
        Bots = new GameObject[BotCount];
        for (int i = 0; i < JoysticCount; i++)
        {
            Players[i] = GameObject.Find("Player" + (i + 1));
        }
        for (int i = 0; i < BotCount; i++)
        {
            Bots[i] = GameObject.Find("Player" + (JoysticCount + i + 1));
        }
        setControllers();
        initialized();

    }

    // Update is called once per frame
    void Update()
    {
        playerEnable();
        CharSelect();
        characterChange();
    }

    void playerEnable()
    {
        if (Input.GetButtonDown("Start_XBOX") && !enabledchar)
        {
            Header.text = "CHOOSE CHARACTERS";
            ReadyText.text = "PRESS 'A' BUTTON TO GET READY";
            enabledchar = true;
            for (int i = 0; i < 4; i++)
            {
                if (TempPlayer > 0)
                {
                    Players[i].SetActive(true);
                    PlayerText[i].text = "Player" + (i + 1);
                    TempPlayer--;
                }
                else
                {
                    Bots[j].SetActive(true);
                    PlayerText[i].text = "Bot" + (j + 1);
                    PlayerReady[JoysticCount + j] = true;
                    PlayerCanvas[JoysticCount + j].SetActive(true);
                    j++;
                }
                PlayerStats[i].SetActive(true);
                PlayerSript[i] = characterArray[1].GetComponent<PlayerMovement>();
                PlayerHealthText[i].text = "Health   " + PlayerSript[i].health;
                PlayerPowerText[i].text = "Power  " + PlayerSript[i].power;
                PlayerStaminaText[i].text = "Stamina  " + PlayerSript[i].stamina;
            }
        }
    }

    void characterChange()
    {
        //Debug.Log(horizontalXbox[0]);
        // In order to prevent multpile character changes at a time when 4th axis of the joystick is activated
        for (int i = 0; i < JoysticCount; i++)
        {
            if (Input.GetAxisRaw(horizontalXbox[i]) == 0)
                PlayerCharChanges[i] = false;

            if (Players[i].activeSelf && characterArray[i * 2].activeSelf && (Input.GetAxisRaw(horizontalXbox[i])) != 0 && !PlayerReady[i] && !PlayerCharChanges[i])
            {
                //keep an index value for each player and deactivate the values except that index value, change the index value when character changes
                PlayerCharChanges[i] = true;
                characterArray[i * 2].SetActive(false);
                characterArray[i * 2 + 1].SetActive(true);
                setCharacterPropertiesOnCanvas(1, i);
            }
            else if (Players[i].activeSelf && characterArray[i * 2 + 1].activeSelf && (Input.GetAxisRaw(horizontalXbox[i])) != 0 && !PlayerReady[i] && !PlayerCharChanges[i])
            {
                PlayerCharChanges[i] = true;
                characterArray[i * 2].SetActive(true);
                characterArray[i * 2 + 1].SetActive(false);
                setCharacterPropertiesOnCanvas(0, i);
            }
        }
        if (PlayerReady[0] && PlayerReady[1] && PlayerReady[2] && PlayerReady[3]) //modify
        {
            SceneManager.LoadScene(1);
        }
    }

    void setCharacterPropertiesOnCanvas(int characterNum, int playerNum)
    {
        for (int i = 0; i < 4; i++)
        {
            if (playerNum == i)
            {
                if (characterNum == 0)
                {
                    PlayerSript[i] = characterArray[0].GetComponent<PlayerMovement>();
                }
                else if (characterNum == 1)
                {
                    PlayerSript[i] = characterArray[1].GetComponent<PlayerMovement>();
                }

                PlayerHealthText[i].text = "Health   " + PlayerSript[i].health;
                PlayerPowerText[i].text = "Power  " + PlayerSript[i].power;
                PlayerStaminaText[i].text = "Stamina  " + PlayerSript[i].stamina;
            }
        }
    }

    private void initialized()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerStats[i].SetActive(false);
            PlayerCanvas[i].SetActive(false);
        }

        for (int i = 0; i < JoysticCount; i++) Players[i].SetActive(false);
        for (int i = 0; i < BotCount; i++) Bots[i].SetActive(false);
        for (int i = 0; i < PlayerReady.Length; i++) PlayerReady[i] = false;

        characterChoices = new int[4];  //maximum 4 players
    }

    void setControllers()
    {
        for (int i = 0; i < JoysticCount; i++)
        {
            if (Input.GetJoystickNames()[i] != "")
            {
                playerControllerNumbers[i] = i + 1; // i + 1 == joystick number 
                //Debug.Log(Input.GetJoystickNames()[i]);
            }
        }

        //Buttons and Axises for XBOX Controllers
        for (int i = 0; i < JoysticCount; i++)
        {
            horizontalXbox[i] = "Horizontal_XBOX_" + playerControllerNumbers[i];
            verticalXbox[i] = "Vertical_XBOX_" + playerControllerNumbers[i];
            submitXbox[i] = "Submit_XBOX_" + playerControllerNumbers[i];
            cameraXbox[i] = "Camera_XBOX_" + playerControllerNumbers[i];
            fireXbox[i] = "Fire_XBOX_" + playerControllerNumbers[i];
            throwableXbox[i] = "Throwable_XBOX_" + playerControllerNumbers[i];
            V_cameraXbox[i] = "V_camera_XBOX_" + playerControllerNumbers[i];
            StaminaXbox[i] = "Stamina_XBOX_" + playerControllerNumbers[i];
            DeathCamXbox[i] = "Death_Cam_XBOX_" + playerControllerNumbers[i];
            Zoomxbox[i] = "Zoom_XBOX_" + playerControllerNumbers[i];
        }
    }

    void CharSelect()
    {
        for (int i = 0; i < JoysticCount; i++)
        {
            if (Input.GetButtonDown(submitXbox[i]) && Players[i].activeSelf) //if first player submits his character, USE AN ARRAY HERE
            {
                //in the character array find the character which is active and set gameobject p1 equal to it (for loop)
                if (characterArray[i * 2].activeSelf)
                {
                    characterChoices[i] = 1;
                    P[i] = characterArray[i * 2]; //not used anywhere but if the prefabs in the character selection and game scene are the same this could be used
                }
                else if (characterArray[i * 2 + 1].activeSelf)
                {
                    characterChoices[i] = 2;
                    P[i] = characterArray[i * 2 + 1];
                }

                PlayerReady[i] = true;
                PlayerCanvas[i].SetActive(true);
            }
        }
    }
}
