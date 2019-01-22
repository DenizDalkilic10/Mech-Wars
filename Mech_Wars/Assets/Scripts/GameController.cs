using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] CharacterArray = new GameObject[4];  // decrease the size to 2 and use the same prefabs for each player
    public PlayerMovement p1_script, p2_script;             // Use this for initialization
   // public PlayerMovement p3_script, p4_script;             // Use this for initialization
    public static GameObject p1, p2;
    //public static GameObject p3, p4;
    //public Text player_1, player_2;
    public RawImage Player_1_img, Player_2_img;
    public RawImage Player_1_sta, Player_2_sta;
    //public RawImage Player_3_img, Player_4_img;
    //public RawImage Player_3_sta, Player_4_sta;

    public static float[] health = new float[4];
    public static float[] stamina = new float[4];
    
    void Start()
    {
        initializeScene();
    }

    // Update is called once per frame
    void Update()
    {     
        Player_1_img.transform.localScale = new Vector3(health[0] / 100, 1, 1);
        Player_2_img.transform.localScale = new Vector3(health[1] / 100, 1, 1);
        Player_1_sta.transform.localScale = new Vector3(stamina[0] / 100, 1, 1);
        Player_2_sta.transform.localScale = new Vector3(stamina[1] / 100, 1, 1);
        //Player_3_img.transform.localScale = new Vector3(health[0] / 100, 1, 1);
        //Player_4_img.transform.localScale = new Vector3(health[1] / 100, 1, 1);
        //Player_3_sta.transform.localScale = new Vector3(stamina[0] / 100, 1, 1);
        //Player_4_sta.transform.localScale = new Vector3(stamina[1] / 100, 1, 1);
    }

    void initializeScene() {

        CharacterSelection.sceneChanged = true;
      
        //for player 1
        if (CharacterSelection.characterChoices[0] == 1)  //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            p1 = CharacterArray[0];
            Instantiate(p1, new Vector3(412, 1, 438), Quaternion.identity);
            Debug.Log("Player1_Instantiated");
        }
        if (CharacterSelection.characterChoices[0] == 2)
        {
            p1 = CharacterArray[1];
            Instantiate(p1, new Vector3(412, 1, 438), Quaternion.identity);
            Debug.Log("Player1_Instantiated");
        }

        //for player 2
        if (CharacterSelection.characterChoices[1] == 1)
        {
            p2 = CharacterArray[2]; //try this with characterArray[0]
            Instantiate(p2, new Vector3(155, 1, 383), Quaternion.identity);
            Debug.Log("Player2_Instantiated");
        }
        if (CharacterSelection.characterChoices[1] == 2)
        {
            p2 = CharacterArray[3]; //try this with characterArray[1]
            Instantiate(p2, new Vector3(155, 1, 383), Quaternion.identity);
            Debug.Log("Player2_Instantiated");
        }

        //for player 3
        //if (CharacterSelection.characterChoices[2] == 1)
        //{
        //    p3 = CharacterArray[0];
        //    Instantiate(p3, new Vector3(155, 1, 383), Quaternion.identity); //change the cooardinates
        //    Debug.Log("Player2_Instantiated");
        //}
        //if (CharacterSelection.characterChoices[2] == 2)
        //{
        //    p3 = CharacterArray[1];
        //    Instantiate(p3, new Vector3(155, 1, 383), Quaternion.identity); //change the cooardinates
        //    Debug.Log("Player2_Instantiated");
        //}


        //for player 4
        //if (CharacterSelection.characterChoices[3] == 1)
        //{
        //    p4 = CharacterArray[0];
        //    Instantiate(p4, new Vector3(155, 1, 383), Quaternion.identity); //change the cooardinates
        //    Debug.Log("Player2_Instantiated");
        //}
        //if (CharacterSelection.characterChoices[3] == 2)
        //{
        //    p4 = CharacterArray[1];
        //    Instantiate(p4, new Vector3(155, 1, 383), Quaternion.identity); //change the cooardinates
        //    Debug.Log("Player2_Instantiated");
        //}

        p1_script = p1.GetComponent<PlayerMovement>();
        p2_script = p2.GetComponent<PlayerMovement>();
        //p3_script = p1.GetComponent<PlayerMovement>();
        //p4_script = p2.GetComponent<PlayerMovement>();

       //if (p1_script.playerName == "Gepard")
       //     player_1.color = Color.red;
       // else if (p1_script.playerName == "Rhino")
       //     player_1.color = Color.green;

       // if (p2_script.playerName == "Gepard")
       //     player_2.color = Color.red;
       // else if (p2_script.playerName == "Rhino")
       //     player_2.color = Color.green;
    }
}
