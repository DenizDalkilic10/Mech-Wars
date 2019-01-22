using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour {

    public static int curentPlayer= 1;
    public int characterNum;

    public int playerNum { get; set; }
    private string horizontalController,verticalController,cameraController;
    
    public string playerName;
    public float stamina;
    public float power;
    public float health;
    public float speed = 100.5f;
	public float turnSpeed = 5f;
    public float cameraRotSpeed;
	private Rigidbody rb;
    public GameObject cameraPivot, gun1_pivot;
    private bool staminaDecrease = false;
    
    public bool splitScreen = false;
    public Camera thirdPersonCamera;

	void Start () 
	{  
		rb = GetComponent <Rigidbody>(); 
        playerNum = curentPlayer;
                                

        if (CharacterSelection.sceneChanged)
        {
            splitScreen = true; //this variable wont be in this class just for checking
            setCameraBoundaries(playerNum);
            curentPlayer++;
            setControllerType(playerNum);
            setCharacterProperties(playerNum);
        }
	}

	void Update () 
	{
        if (CharacterSelection.sceneChanged)
        {
            if (Input.GetButtonDown("Jump") && stamina != 0)
            {
                speed *= 5;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                speed *= 0.2f;
            }

            if (Input.GetButton("Jump") && this.stamina > 0)
                this.stamina -= 0.2f;
            else if(this.stamina < 100)
                this.stamina += 0.05f;

            rb.transform.Rotate(0.0f, Input.GetAxis(horizontalController) * turnSpeed, 0.0f);
            transform.Translate((transform.right * Input.GetAxis(verticalController)) * speed * Time.deltaTime, Space.World);
            cameraPivot.transform.Rotate(0.0f, Input.GetAxis(cameraController) * cameraRotSpeed, 0.0f); 
        }

        if (Input.GetKeyDown("c") && thirdPersonCamera.gameObject.activeSelf)
        {
            thirdPersonCamera.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown("c") && !(thirdPersonCamera.gameObject.activeSelf))
        {
            thirdPersonCamera.gameObject.SetActive(true);
        }
        GameController.health[playerNum-1] = this.health;
        GameController.stamina[playerNum-1] = this.stamina;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            Destroy(collision.gameObject);
            this.health -= 5;     
        }

        if (this.health <= 0)
            Destroy(gameObject);

        if (collision.gameObject.name == "axe")
        {
            Axe s = collision.gameObject.GetComponent<NewAxeCollisions>().axe;
            Debug.Log(s.playerNumber + " --- " + this.playerNum );
            if (s.playerNumber != this.playerNum)
                this.health -= 10;
            if (this.health < 0)
                this.health = 0;
        }
    }

    void setControllerType(int playerNum)
    { 
        //Edit this method whenever a new control is added
        horizontalController =  CharacterSelection.horizontalXbox[playerNum-1];
        verticalController =  CharacterSelection.verticalXbox[playerNum-1];
        cameraController = CharacterSelection.cameraXbox[playerNum - 1];
    }

    void setCharacterProperties(int playerNum)
    {
        if (CharacterSelection.characterChoices[playerNum - 1] == 1)
        {
            this.playerName = "Gepard";
            this.stamina = 100;
            this.power = 100;
            this.health = 100;
            this.speed = 100f;
            this.turnSpeed = 1f;
            this.cameraRotSpeed = 2.5f;
        }
        else if (CharacterSelection.characterChoices[playerNum - 1] == 2)
        {
            //To Do adjust the values later
            this.playerName = "Rhino";
            this.stamina = 100;
            this.power = 100;
            this.health = 100;
            this.speed = 100f;
            this.turnSpeed = 1f;
            this.cameraRotSpeed = 2.5f;
        }
    }

    void setCameraBoundaries(int num) //access the total number of current players and fix this method
    {
        if (splitScreen)
        {
            thirdPersonCamera.gameObject.SetActive(true);  //attached from the editor
            
            if (num == 1)
            {
                thirdPersonCamera.rect = new Rect(0, 0, 0.5f, 1);
            }
            else if (num == 2)
            {
                thirdPersonCamera.rect = new Rect(0.5f, 0, 0.5f, 1);
            }
        }
    }

    public void changePlayerNum(int num)
    {
        playerNum = num;
    }
}