using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //float bridgetimer = 0.0f;
    float PlatformSpeed = 5.0f; //Platform speed
    float zLimit = 39.0f; //Platform Z limit on up
    float RezLimit = 31.0f; //Platform Z limit on down
    bool PlusLimit = true; //To check if platform has reach their limits
    float PowerUp; //Calling PowerUp var as float

    Renderer Character; // To access Libary for colour
    public GameObject Child; // Public game object for you to assign. Check Unity
    public Material[] CharacterMaterials; //An array declared for you to put assign array size and material publicly in unity.

    //public GameObject Parent;

    bool MovingPlat = false; //Moving platform condition

    bool BridgeOn; //To check if bridge is turned on or not

    float timerCount = 7.0f; // Timer for bridge
    int timeCountInt; // To make the number int

    public GameObject timerText;//Public gameobject for you to tag, check your unity

    public GameObject MovingPlatform; //Public gameobject for you to tag, check your unity


    float JumpForce = 5.0f; //Jump power of player
    float speed = 5.0f; //Walkspeed of player
    bool isOnFloor; //Bool to check if player is on floor or not
    Rigidbody rb; //Player RigidBody
    public Animator PlayerAnimin; //Publiclly Assign a Animator
    public GameObject Bridge; //Assigning a game object to the name bridge

    // Start is called before the first frame update
    void Start()
    {
        Character = Child.GetComponent<SkinnedMeshRenderer>(); // Character is the renderer and child.getcomponent is referencing the object that's tagged child.
        rb = GetComponent<Rigidbody>();
        //PlayerAnimin = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) // Moving forward via W also GetKey constantly is active until you release the key
        {
            PlayerAnimin.SetFloat("RunningStateFloat", 2.6f);
            PlayerAnimin.SetBool("RunningStateBool", true);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.S)) // Moving back via S also GetKey constantly is active until you release the key
        {
            PlayerAnimin.SetFloat("RunningStateFloat", 2.6f);
            PlayerAnimin.SetBool("RunningStateBool", true);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.A)) // Moving left via A also GetKey constantly is active until you release the key
        {
            PlayerAnimin.SetFloat("RunningStateFloat", 2.6f);
            PlayerAnimin.SetBool("RunningStateBool", true);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (Input.GetKey(KeyCode.D)) // Moving right via D also GetKey constantly is active until you release the key
        {
            PlayerAnimin.SetFloat("RunningStateFloat", 2.6f);
            PlayerAnimin.SetBool("RunningStateBool", true);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnFloor == true) // Space to jump and checking if player is indeed in the air
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            PlayerAnimin.SetTrigger("JumpingStateTrigger");
            Character.material.color = CharacterMaterials[1].color;
            isOnFloor = false;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) //Check if player have released the W or S key
        {
            PlayerAnimin.SetFloat("RunningStateFloat", 0);
            PlayerAnimin.SetBool("RunningStateBool", false);
        }

        if (MovingPlat == true) //Checking if the moving Platform has been activated
        {
            if (MovingPlatform.transform.position.z < zLimit && PlusLimit == true) //Checking to see if the platform is over to the zLimit stated
            {
                MovingPlatform.transform.Translate(Vector3.forward * Time.deltaTime * PlatformSpeed);

            }
            else if (MovingPlatform.transform.position.z > RezLimit && PlusLimit == false) //Checking to see if the platform is over to the RezLimit stated
            {
                MovingPlatform.transform.Translate(Vector3.forward * Time.deltaTime * -PlatformSpeed);
            }
            else
            {
                PlusLimit = !PlusLimit; //Changing PlusLimit into false
            }
        }
        timerText.GetComponent<Text>().text = "Timer: " + timeCountInt; //Counting and printing the time to text object

        if (BridgeOn == true) 
        {
            if (timerCount > 0) // If timer is greater than 0 perform the following
            {
                timerCount -= Time.deltaTime; // Subtructing 1 per second
                timeCountInt = Mathf.RoundToInt(timerCount); //Converting float to int
            }

            else if (timerCount < 1) // If timer is lower than 1 perform the following
            {
                Bridge.transform.rotation = Quaternion.Euler(0, -90, 0); //Rotate the bridge to y -90
                BridgeOn = false;//Set bridge activation to false
            }

        }
        if (BridgeOn == false)//If bridge is not activated then reset timer to 7 seconds
        {
            timerCount = 7.0f;
        }

        if (transform.position.y < -5)
        {
            SceneManager.LoadScene("LostScene"); //Moving player to LostScene if player's Y value call below -5
        }




    }

    private void OnCollisionEnter(Collision Player) 
    {
        if(Player.gameObject.CompareTag("Floor")) //Ensuring player is touching the object with floor tag so you cannot double jump
        {
            isOnFloor = true;
            Character.material.color = CharacterMaterials[0].color;
        }

        if (Player.gameObject.CompareTag("Moving Platform")) //Ensuring player is touching the object with moving platform tag so you cannot double jump
        {
            isOnFloor = true;
            Character.material.color = CharacterMaterials[0].color;
        }
        if (Player.gameObject.CompareTag("Bridge")) //Ensuring player is touching the object with bridge tag so you cannot double jump
        {
            isOnFloor = true;
            Character.material.color = CharacterMaterials[0].color;
        }

        if (Player.gameObject.CompareTag("Cone") && PowerUp >= 4) // If player touch cone execute the following code
        {
            BridgeOn = true;
            //GameObject.FindGameObjectWithTag("Bridge").transform.rotation = Quaternion.Euler(0, 0, 0);
            Bridge.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            MovingPlat = true;
            //Debug.Log("Hi"); For debugging
        }

        if(other.gameObject.tag == "PowerUp")
        {
            Destroy(other.gameObject);
            PowerUp++; //Increase powerup picked when picked up
        }
    }

}
