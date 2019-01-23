using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{

    //VARIABLES

    //int
    public static int curentPlayer = 1;
    public int characterNum;
    public int playerNum { get; set; }

    //float
    public float stamina;
    public float power;
    public float health;
    public float speed = 1f;
    public float turnSpeed = 5f;
    public float cameraRotSpeed;
    public BotController agent;

    //bool
    private bool staminaDecrease = false;
    public bool isBot = false;
    public bool splitScreen = false;

    //string
    private string horizontalController, verticalController, cameraController, V_cameraController, StaminaXbox, DeathcamXbox, ZoomXbox;
    public string playerName;

    //GameObject
    private Rigidbody rb;
    public GameObject cameraPivot;
    public GameObject Dead;
    public Camera thirdPersonCamera;
    public Camera mainCam;
    DeathCam dc;

    float yRot = 0.0f;
    float zRot = 0.0f;
    float timerstamina = 5f;

    //script
    private GameController gc;

    void Start()
    {
        
        if (GameObject.Find("GameController"))
            gc = GameObject.Find("GameController").GetComponent<GameController>();

        rb = GetComponent<Rigidbody>();
        playerNum = curentPlayer;

        if (isBot)
            playerNum = 0;
        else
            playerNum = curentPlayer;

        if (CharacterSelection.sceneChanged)
        {
            splitScreen = true; //this variable wont be in this class just for checking
            setCharacterProperties(playerNum);
            if (!isBot)
            {
                setCameraBoundaries(playerNum);
                curentPlayer++;
                setControllerType(playerNum);
            }
        }

    }

    void Update()
    {
        timerstamina += Time.deltaTime;
        if (!isBot)
        {
            if (CharacterSelection.sceneChanged)
            {
                if (Input.GetAxis(StaminaXbox) != 0 && this.stamina > 0 && timerstamina > 5)
                {
                    this.speed = 3;
                    this.stamina -= 0.2f;
                }
                else if (this.stamina < 100)
                {
                    this.stamina += 0.05f;
                    this.speed = 1;
                }
                if (this.stamina <= 0)
                {
                    timerstamina = 0;
                }


                //controllers
                rb.transform.Rotate(0.0f, Input.GetAxis(horizontalController) * turnSpeed, 0.0f);
                //transform.Translate((transform.right * Input.GetAxis(verticalController)) * speed * Time.deltaTime, Space.World);
                //rb.AddForce(transform.right * Input.GetAxis(verticalController) * speed);
                //rb.transform.Translate(Input.GetAxis(verticalController) * speed*Time.deltaTime,0f,0f);
                rb.MovePosition(this.transform.position + transform.right * Input.GetAxis(verticalController) * speed);
                GameController.health[playerNum - 1] = this.health; 
                GameController.stamina[playerNum - 1] = this.stamina;
                yRot += Input.GetAxis(cameraController) * cameraRotSpeed * Time.deltaTime;
                zRot += Input.GetAxis(V_cameraController) * cameraRotSpeed * Time.deltaTime;
                if (zRot > 330)
                {
                    zRot -= 360;
                }
                zRot = Mathf.Clamp(zRot, -20f, 25f);
                cameraPivot.transform.localEulerAngles = new Vector3(0f, yRot, -zRot);
                if (Input.GetButton(ZoomXbox))
                {
                    mainCam.fieldOfView = 30;
                }
                else
                {
                    mainCam.fieldOfView = 60;
                }
            }
        }

        if (this.health <= 0)
        {
            if (!isBot)
            {
                
                GameObject deadobject = Instantiate(Dead, this.transform.position, Quaternion.identity);
                dc = deadobject.GetComponent<DeathCam>();
                dc.DeathCamController1 = DeathcamXbox;
                Camera deadcam = deadobject.transform.GetChild(0).GetComponent<Camera>();
                deadcam.rect = mainCam.rect;
                gc.canvasArray[playerNum - 1].enabled = false;
                Destroy(gameObject);
            }
            else
            {
                GameObject deadobject = Instantiate(Dead, this.transform.position, Quaternion.identity);
                //dc = deadobject.GetComponent<DeathCam>();
                //dc.DeathCamController1 = DeathcamXbox;
                //Camera deadcam = deadobject.transform.GetChild(0).GetComponent<Camera>();
                //deadcam.rect = mainCam.rect;
                //gc.canvasArray[playerNum - 1].enabled = false;
                Destroy(gameObject.transform.parent.gameObject);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            Destroy(collision.gameObject);
            this.health -= 5;
            if (isBot)
            {
                agent.randomDestination();
            }
        }

        if (collision.gameObject.name == "axe")
        {
            Axe s = collision.gameObject.GetComponent<NewAxeCollisions>().axe;
            Debug.Log(s.playerNumber + " --- " + this.playerNum);

            if (s.playerNumber != this.playerNum)
                this.health -= 5;

            if (this.health < 0)
                this.health = 0;
        }

        if (collision.gameObject.name == "healthPU(Clone)")
        {
            Destroy(collision.gameObject);
            GameController.healthPuPresent = false;
            GameController.timeLastHealthPU = Time.time;

            if (playerNum == 0 || this.gameObject.name == "gepardFinal(Clone)")
            {
                if (this.health <= 60)
                    this.health += 20;
                else
                    this.health = 100;
            }
            else if(this.gameObject.name == "rhinoFinal(Clone)")
            {
                if (this.health <= 100)
                    this.health += 20;
                else
                    this.health = 150;
            }


            if (agent)
            {
                agent.GoingTowardsPowerUp = false;
                agent.IsnonTargetDestinationSet1 = false;
            }
        }

        if (collision.gameObject.name == "staminaPU(Clone)")
        {
            Destroy(collision.gameObject);
            GameController.staminaPuPresent = false;
            GameController.timeLastStaminaPU = Time.time;

            if (this.gameObject.name == "gepardFinal(Clone)")
            {
                if (this.stamina <= 80)
                    this.stamina += 50;
                else
                    this.stamina = 150;
            }
            else if (playerNum == 0 || this.gameObject.name == "rhinoFinal(Clone)")
            {
                if (this.stamina <= 50)
                    this.stamina += 50;
                else
                    this.stamina = 100;
            }

            if (agent)
            {
                agent.GoingTowardsPowerUp = false;
                agent.IsnonTargetDestinationSet1 = false;
            }
        }

    }

    void setControllerType(int playerNum)
    {
        //Edit this method whenever a new control is added
        horizontalController = CharacterSelection.horizontalXbox[playerNum - 1];
        verticalController = CharacterSelection.verticalXbox[playerNum - 1];
        cameraController = CharacterSelection.cameraXbox[playerNum - 1];
        V_cameraController = CharacterSelection.V_cameraXbox[playerNum - 1];
        StaminaXbox = CharacterSelection.StaminaXbox[playerNum - 1];
        DeathcamXbox = CharacterSelection.DeathCamXbox[playerNum - 1];
        ZoomXbox = CharacterSelection.Zoomxbox[playerNum - 1];
    }

    void setCharacterProperties(int playerNum)
    {
        if (playerNum == 0)
        {
            this.playerName = "Bot";
            this.stamina = 100;
            this.power = 100;
            this.health = 100;
            this.speed = 1f;
            this.turnSpeed = 1f;
            this.cameraRotSpeed = 40f;
        }
        else if (CharacterSelection.characterChoices[playerNum - 1] == 1)
        {
            this.playerName = "Gepard";
            this.stamina = 150;
            this.power = 100;
            this.health = 100;
            this.speed = 1f;
            this.turnSpeed = 1.5f;
            this.cameraRotSpeed = 40f;
        }
        else if (CharacterSelection.characterChoices[playerNum - 1] == 2)
        {
            //To Do adjust the values later
            this.playerName = "Rhino";
            this.stamina = 100;
            this.power = 100;
            this.health = 150;
            this.speed = 1f;
            this.turnSpeed = 1.5f;
            this.cameraRotSpeed = 40f;
        }

    }

    void setCameraBoundaries(int num) //access the total number of current players and fix this method
    {
        //TRY THIS FIRST

        //if there is only 1 player the camera will be whole screen, else if 2 split screen ... and so on

        if (splitScreen)
        {
            thirdPersonCamera.gameObject.SetActive(true);  //attached from the editor

            if (CharacterSelection.JoysticCount == 1)
            {
                thirdPersonCamera.rect = new Rect(0, 0, 1, 1);
            }
            else if (CharacterSelection.JoysticCount == 2)
            {
                if (num == 1)
                {
                    thirdPersonCamera.rect = new Rect(0, 0, 0.5f, 1);
                }
                else if (num == 2)
                {
                    thirdPersonCamera.rect = new Rect(0.5f, 0, 0.5f, 1);
                }
            }
            else if (CharacterSelection.JoysticCount == 3)
            {
                if (num == 1)
                {
                    thirdPersonCamera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                }
                else if (num == 2)
                {
                    thirdPersonCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                }
                else if (num == 3)
                {
                    thirdPersonCamera.rect = new Rect(0, 0, 1f, 0.5f);
                }
            }
            else if (CharacterSelection.JoysticCount == 4)
            {
                if (num == 1)
                {
                    thirdPersonCamera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                }
                else if (num == 2)
                {
                    thirdPersonCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                }
                else if (num == 3)
                {
                    thirdPersonCamera.rect = new Rect(0, 0, 0.5f, 0.5f);
                }
                else if (num == 4)
                {
                    thirdPersonCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                }
            }
        }
    }

    public void changePlayerNum(int num)
    {
        playerNum = num;
    }
}