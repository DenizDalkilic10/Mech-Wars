using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    //variables
    public GameObject[] characterArray = new GameObject[4]; // twice the times of characters
    public GameObject player1, player2, p1_stats, p2_stats;
    //public GameObject player3, player4, p3_stats, p4_stats;
    public Text p1_Text, p2_Text, health_1, health_2, power_1, power_2, stamina_1, stamina_2, continueText;
    //public Text p3_Text, p4_Text, health_3, health_4, power_3, power_4, stamina_3, stamina_4;
    private PlayerMovement player1_script, player2_script;
    //private PlayerMovement player3_script, player4_script;
    public GameObject readyCanvas, readyCanvas2;
    //public GameObject readyCanvas3, readyCanvas4;
    private bool player1ready, player2ready;
    //private bool player3ready, player4ready;
    public static bool sceneChanged = false;
    private bool player1_character_change, player2_character_change;
    //private bool player3_character_change, player4_character_change;

    //controller numbers corresponding player indexes
    public static int[] playerControllerNumbers = new int[4];  //most xbox can handle

    //controllers for each button
    public static string[] submitXbox = new string[7];
    public static string[] horizontalXbox = new string[7];
    public static string[] verticalXbox = new string[7];
    public static string[] cameraXbox = new string[7];
    public static string[] fireXbox = new string[7];
    public static string[] throwableXbox = new string[7];



    [HideInInspector]
    public static int[] characterChoices; //size of this array will be equal to maximum number of players

    // Use this for initialization
    void Start()
    {
        
        Debug.Log("Start");
        Debug.Log(Input.GetJoystickNames().Length);

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            Debug.Log(Input.GetJoystickNames()[i] + "-");

        setControllers();
        player1_character_change = false;
        player2_character_change = false;
        //player3_character_change = false;
        //player4character_change = false;
        initialized();
    }

    // Update is called once per frame
    void Update()
    {
        playerEnable();
        characterChange();
    }

    void playerEnable()
    {
        if (!player1.activeSelf && !player2.activeSelf && Input.GetButtonDown("Start_XBOX"))
        {
            player1.SetActive(true);
            p1_Text.text = "    Player 1";
            p1_stats.SetActive(true);
            player1_script = characterArray[1].GetComponent<PlayerMovement>();
            health_1.text = "Health   " + player1_script.health;
            power_1.text = "Power  " + player1_script.power;
            stamina_1.text = "Stamina  " + player1_script.stamina;

        }
        else if (player1.activeSelf && !player2.activeSelf && Input.GetButtonDown("Start_XBOX"))
        {
            player2.SetActive(true);
            p2_Text.text = "    Player 2";
            p2_stats.SetActive(true);
            player2_script = characterArray[3].GetComponent<PlayerMovement>();
            health_2.text = "Health   " + player2_script.health;
            power_2.text = "Power  " + player2_script.power;
            stamina_2.text = "Stamina  " + player2_script.stamina;
        }
        //else if(player1.activeSelf && player2.activeSelf && !player3.activeSelf && Input.GetButtonDown("Start_XBOX"))
        //{
        //    player3.SetActive(true);
        //    p3_Text.text = "    Player 2";
        //    p3_stats.SetActive(true);
        //    player3_script = characterArray[3].GetComponent<PlayerMovement>();  //fix the character array
        //    health_3.text = "Health   " + player3_script.health;
        //    power_3.text = "Power  " + player3_script.power;
        //    stamina_3.text = "Stamina  " + player3_script.stamina;
        //}
        //else if (player1.activeSelf && player2.activeSelf && player3.activeSelf && !player4.activeSelf Input.GetButtonDown("Start_XBOX"))
        //{
        //    player4.SetActive(true);
        //    p4_Text.text = "    Player 2";
        //    p4_stats.SetActive(true);
        //    player4_script = characterArray[3].GetComponent<PlayerMovement>(); //character array
        //    health_4.text = "Health   " + player4_script.health;
        //    power_4.text = "Power  " + player4_script.power;
        //    stamina_4.text = "Stamina  " + player4_script.stamina;
        //}
    }

    void characterChange()
    {
        //Debug.Log(horizontalXbox[0]);
        // In order to prevent multpile character changes at a time when 4th axis of the joystick is activated
        if (Input.GetAxisRaw(horizontalXbox[0]) == 0)
            player1_character_change = false;    
        

        if (Input.GetAxisRaw(horizontalXbox[1]) == 0)
            player2_character_change = false;

        //if (Input.GetAxisRaw(horizontalXbox[2]) == 0)
        //    player3_character_change = false;


        //if (Input.GetAxisRaw(horizontalXbox[3]) == 0)
        //    player4_character_change = false;

        //Change This Structure To an array structure as it will be 4 players and there may be many characters
        if (player1.activeSelf && characterArray[0].activeSelf && (Input.GetAxisRaw(horizontalXbox[0])) != 0 && !player1ready && !player1_character_change)
        {
            //keep an index value for each player and deactivate the values except that index value, change the index value when character changes
            player1_character_change = true;
            characterArray[0].SetActive(false);
            characterArray[1].SetActive(true);
            setCharacterPropertiesOnCanvas(1, 1);
        }
        else if (player1.activeSelf && characterArray[1].activeSelf && (Input.GetAxisRaw(horizontalXbox[0])) != 0 && !player1ready && !player1_character_change)
        {
            player1_character_change = true;
            characterArray[0].SetActive(true);
            characterArray[1].SetActive(false);
            setCharacterPropertiesOnCanvas(0, 1);
        }
        else if (player2.activeSelf && characterArray[2].activeSelf && (Input.GetAxisRaw(horizontalXbox[1])) != 0 && !player2ready && !player2_character_change)
        {
            player2_character_change = true;
            characterArray[2].SetActive(false);
            characterArray[3].SetActive(true);
            setCharacterPropertiesOnCanvas(3, 2);
        }
        else if (player2.activeSelf && characterArray[3].activeSelf && (Input.GetAxisRaw(horizontalXbox[1])) != 0 && !player2ready && !player2_character_change)
        {
            player2_character_change = true;
            characterArray[2].SetActive(true);
            characterArray[3].SetActive(false);
            setCharacterPropertiesOnCanvas(2, 2);
        }
        //else if (player3.activeSelf && characterArray[4].activeSelf && (Input.GetAxisRaw(horizontalXbox[2])) != 0 && !player3ready && !player3_character_change)
        //{
        //    player3_character_change = true;
        //    characterArray[4].SetActive(false);
        //    characterArray[5].SetActive(true);
        //    setCharacterPropertiesOnCanvas(5, 3); //check
        //}
        //else if (player3.activeSelf && characterArray[5].activeSelf && (Input.GetAxisRaw(horizontalXbox[2])) != 0 && !player3ready && !player3_character_change)
        //{
        //    player2_character_change = true;
        //    characterArray[4].SetActive(true);
        //    characterArray[5].SetActive(false);
        //    setCharacterPropertiesOnCanvas(4, 3); //check
        //}
        //else if (player4.activeSelf && characterArray[6].activeSelf && (Input.GetAxisRaw(horizontalXbox[3])) != 0 && !player4ready && !player4_character_change)
        //{
        //    player4_character_change = true;
        //    characterArray[6].SetActive(false);
        //    characterArray[7].SetActive(true);
        //    setCharacterPropertiesOnCanvas(7, 4); //check
        //}
        //else if (player4.activeSelf && characterArray[7].activeSelf && (Input.GetAxisRaw(horizontalXbox[3])) != 0 && !player4ready && !player4_character_change)
        //{
        //    player4_character_change = true;
        //    characterArray[6].SetActive(true);
        //    characterArray[7].SetActive(false);
        //    setCharacterPropertiesOnCanvas(6, 4); //check
        //}

        if (Input.GetButtonDown(submitXbox[0])) //if first player submits his character, USE AN ARRAY HERE
        {
            //in the character array find the character which is active and set gameobject p1 equal to it (for loop)
            if (characterArray[0].activeSelf)
            {
                characterChoices[0] = 1;
                GameObject p1 = characterArray[0];
            }
            else if (characterArray[1].activeSelf)
            {
                characterChoices[0] = 2;
                GameObject p1 = characterArray[1];
            }

            player1ready = true;
            readyCanvas.SetActive(true);
        }

        if (Input.GetButtonDown(submitXbox[1]))
        {
            if (characterArray[2].activeSelf)
            {
                characterChoices[1] = 1;  //character index 0 (first player) chooses first character(1) 1 == rhino , 2 == gepard 
                GameObject p2 = characterArray[2];
            }
            else if (characterArray[3].activeSelf)
            {
                characterChoices[1] = 2;
                GameObject p2 = characterArray[3];
            }

            player2ready = true;
            readyCanvas2.SetActive(true);
        }

        //if (Input.GetButtonDown(submitXbox[2]))
        //{
        //    if (characterArray[4].activeSelf)
        //    {
        //        characterChoices[2] = 1;  //character index 0 (first player) chooses first character(1) 1 == rhino , 2 == gepard 
        //        GameObject p3 = characterArray[4];
        //    }
        //    else if (characterArray[5].activeSelf)
        //    {
        //        characterChoices[2] = 2;
        //        GameObject p3 = characterArray[5];
        //    }

        //    player3ready = true;
        //    readyCanvas3.SetActive(true);
        //}

        //if (Input.GetButtonDown(submitXbox[3]))
        //{
        //    if (characterArray[6].activeSelf)
        //    {
        //        characterChoices[3] = 1;  //character index 0 (first player) chooses first character(1) 1 == rhino , 2 == gepard 
        //        GameObject p4 = characterArray[6];
        //    }
        //    else if (characterArray[7].activeSelf)
        //    {
        //        characterChoices[3] = 2;
        //        GameObject p4 = characterArray[7];
        //    }

        //    player4ready = true;
        //    readyCanvas4.SetActive(true);
        //}

        if (player1ready && player2ready) // && player2ready && player4ready
        {
            continueText.text = "Press Start To Start";
        }

        if (player1ready && player2ready && Input.GetButton("Start_XBOX")) // && player2ready && player4ready
        {
            SceneManager.LoadScene(1);
        }
    }

    void setCharacterPropertiesOnCanvas(int characterNum, int playerNum) {
        if (playerNum == 1)
        {
            if (characterNum == 0)
            {
                player1_script = characterArray[0].GetComponent<PlayerMovement>();
            }
            else if (characterNum == 1)
            {
                player1_script = characterArray[1].GetComponent<PlayerMovement>();
            }

            health_1.text = "Health   " + player1_script.health;
            power_1.text = "Power  " + player1_script.power;
            stamina_1.text = "Stamina  " + player1_script.stamina;
        }
        else if (playerNum == 2)
        {
            if (characterNum == 2)
            {
                player2_script = characterArray[2].GetComponent<PlayerMovement>();
            }
            else if (characterNum == 3)
                player2_script = characterArray[3].GetComponent<PlayerMovement>();

            health_2.text = "Health   " + player2_script.health;
            power_2.text = "Power  " + player2_script.power;
            stamina_2.text = "Stamina  " + player2_script.stamina;
        }
        //else if (playerNum == 3)
        //{
        //    if (characterNum == 4)
        //    {
        //        player3_script = characterArray[4].GetComponent<PlayerMovement>();
        //    }
        //    else if (characterNum == 5)
        //        player3_script = characterArray[5].GetComponent<PlayerMovement>();

        //    health_3.text = "Health   " + player3_script.health;
        //    power_3.text = "Power  " + player3_script.power;
        //    stamina_3.text = "Stamina  " + player3_script.stamina;
        //}
        //else if (playerNum == 4)
        //{
        //    if (characterNum == 6)
        //    {
        //        player2_script = characterArray[6].GetComponent<PlayerMovement>();
        //    }
        //    else if (characterNum == 7)
        //        player2_script = characterArray[7].GetComponent<PlayerMovement>();

        //    health_4.text = "Health   " + player4_script.health;
        //    power_4.text = "Power  " + player4_script.power;
        //    stamina_4.text = "Stamina  " + player4_script.stamina;
        //}
    }

    private void initialized()
    {
        p1_Text.text = "Press Space To Activate";
        p2_Text.text = "Press Space To Activate";
        //p3_Text.text = "Press Space To Activate";
        //p4_Text.text = "Press Space To Activate";
        player1ready = false;
        player2ready = false;
        //player3ready = false;
        //player4ready = false;
        player1.SetActive(false);
        player2.SetActive(false);
        //player3.SetActive(false);
        //player4.SetActive(false);
        p1_stats.SetActive(false);
        p2_stats.SetActive(false);
        //p3_stats.SetActive(false);
        //p4_stats.SetActive(false);
        readyCanvas.SetActive(false);
        readyCanvas2.SetActive(false);
        //readyCanvas3.SetActive(false);
        //readyCanvas4.SetActive(false);
        characterChoices = new int[4];  //maximum 4 players
    }

    void setControllers()
    {
        int index = 0;
        for (int i = 0; i < Input.GetJoystickNames().Length; i++) {
            if (Input.GetJoystickNames()[i] != "")
            {
                playerControllerNumbers[index] = i+1; // i + 1 == joystick number
                index++;
            }
        }

        for (int i = 0; i < 4; i++)
            Debug.Log(playerControllerNumbers[i] + "-");

        //Buttons and Axises for XBOX Controllers

        for (int i = 0; i < 4; i++)
            horizontalXbox[i] = "Horizontal_XBOX_" + playerControllerNumbers[i];
        for (int i = 0; i < 4; i++)
            verticalXbox[i] = "Vertical_XBOX_" + playerControllerNumbers[i];
        for (int i = 0; i < 4; i++)
            submitXbox[i] = "Submit_XBOX_" + playerControllerNumbers[i];
        for (int i = 0; i < 4; i++)
            cameraXbox[i] = "Camera_XBOX_" + playerControllerNumbers[i];
        for (int i = 0; i < 4; i++)
            fireXbox[i] = "Fire_XBOX_" + playerControllerNumbers[i];
        for (int i = 0; i < 4; i++)
            throwableXbox[i] = "Throwable_XBOX_" + playerControllerNumbers[i];
    }
}
