using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using Unity.Hierarchy;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector2 mousePosition;

    //Screens

    public GameObject gameOverScreen;
    public GameObject getReadyScreen;
    public TextMeshProUGUI gameOverText;
    public GameObject introPage;
    public GameObject score;

    //Objects
    public Rigidbody2D rigidbodyModule;
    public Character player;
    public GameObject playerCharacter;
    public Button button;
    public GameObject Levelf;
    public GameObject Levelb;
    private GameObject LevelClonef;
    private GameObject LevelCloneb;
    //Data
    public TextMeshProUGUI coinNum;
    public TextMeshProUGUI livesText;
    private Vector2 checkPoint;



    public int lives;
    public float coins;
    public int force;
    public float timeleft;


    //the next plan under here is to make the camera follow the player, but not have it rotate with the player. I think what I can do is make it so that the camera will follow the player if player isDead is false, and then when its true it will revert to the full screen camera until the main menu is shown.
    public GameObject camera1;
    public GameObject camera2;

    //this is the component to call the camera stuff
    private GameObject mCamera;
    private Camera cameraComponent;
    private Vector3 newPos;
    //This is going to be for the number of jumps 
    public int numJumps;
    public GameObject jumpCanvas;
    public TextMeshProUGUI numOJ;
    public float numberTimer;




    void Start()
    {
        timeleft = 0;
        numberTimer = 0;
        coins = 0;
        lives = 3;
        button.onClick.AddListener(ButtonClick);
        player.isDead = true;
        mCamera = camera1;
        player.jumping(false);
        
    }

    //create functions for getting coins and for restarting the game.

    public void GetCoin()
    {
        coins += 1;
        coinNum.text = coins.ToString();
    }

    public void GetReady()
    {
        introPage.SetActive(false);
        score.SetActive(false);
        getReadyScreen.SetActive(true);
        timeleft = 3;

    }

    public void RespawnPlayer()
    {
        //Once checkpoints are added, work on adding the players respawn in this function, since this will reset all of what comes with the level if ever I get to that point.
        playerCharacter.transform.position = checkPoint;
        playerCharacter.SetActive(true);
        player.isDead = false;


        score.SetActive(true);


    }

    public void SetSpawnPoint(Vector2 pos)
    {
        checkPoint = pos;

    }

    public void Died()
    {
        if (lives == 0)
        {
            GameOver();

        }
        else
        {
            playerCharacter.SetActive(false);
            GetReady();
        }
    }


    public void GameOver()
    {
        timeleft = 5;
        player.isDead = true;
        playerCharacter.transform.position = new Vector2(0, 0);
        playerCharacter.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void ButtonClick()
    {
        introPage.SetActive(false);
        gameOverText.text = "Game Over";
        coins = 0;
        lives = 3;
        numJumps = 3;
        coinNum.text = coins.ToString();
        checkPoint = new Vector2(0, 0);
        LevelClonef = Instantiate(Levelf);
        LevelCloneb = Instantiate(Levelb);
        GetReady();
    }

    public void bodyMovement(GameObject obj, float limit, float order, string special)
    {

        Vector3 origin = new Vector3(0, 0, order);

        
        origin = origin + ((Vector3)mousePosition - playerCharacter.transform.position)/ 75;
        if (special == "PUPIL")
        {

            origin = origin + ((Vector3)mousePosition - playerCharacter.transform.position) / 3000;

            if (origin.x > 0.022)
            {
                origin.x = 0.022f;

            }
            if (origin.x < -0.022)
            {
                origin.x = -0.022f;

            }
            if (origin.y > 0.028)
            {
                origin.y = 0.028f;

            }
            if (origin.y < -0.028)
            {
                origin.y = -0.028f;

            }
        }

        origin.z = order;
        if (special == "LEFT")
        {
            origin.x += -0.225f;
            origin.y += 0.087f;

        }
        if (special == "RIGHT")
        {
            origin.x += 0.218f;
            origin.y += -0.004f;

        }
        obj.transform.localPosition = origin;

        if (special == "NONE")
        {
            obj.transform.up = (Vector3)mousePosition - playerCharacter.transform.position;
        }
    }


    // Update is called once per frame

    void Update()
    {

        livesText.text = lives.ToString();
        cameraComponent = mCamera.GetComponent<Camera>();

        
        numOJ.text = numJumps.ToString();



        mousePosition = cameraComponent.ScreenToWorldPoint(Input.mousePosition);





        //Character Appendage Control

        playerCharacter.transform.rotation = Quaternion.identity;
        bodyMovement(player.smallBack, 0.15f, player.smallBack.transform.position.z, "NONE");
        bodyMovement(player.smallFront, 0.15f, player.smallFront.transform.position.z, "NONE");
        bodyMovement(player.leftEye, 0.15f, player.leftEye.transform.position.z, "LEFT");
        bodyMovement(player.rightEye, 0.15f, player.rightEye.transform.position.z, "RIGHT");
        bodyMovement(player.leftPupil, 0.15f, player.leftEye.transform.position.z, "PUPIL");
        bodyMovement(player.rightPupil, 0.15f, player.rightEye.transform.position.z, "PUPIL");




        //camera control

        if (player.isDead == false)
        {
            mCamera = camera2;
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
        if (player.isDead == true)
        {
            mCamera = camera1;
            camera1.SetActive(true);
            camera2.SetActive(false);
        }

        //Camera cant go a set amount away from the player

        newPos = (Vector2)playerCharacter.transform.position - (( (Vector2)playerCharacter.transform.position - mousePosition) /4);
        newPos.z = -10;



        camera2.transform.rotation = Quaternion.identity;
        camera2.transform.position = newPos;

        //Time to test raycasting, player should only have 3 jumps until they touch the floor again

        Vector2 castDirection;
        castDirection = playerCharacter.transform.position;
        castDirection.y -= 0.50f;

        LayerMask wall = LayerMask.GetMask("Obstacles");
        RaycastHit2D hit = Physics2D.Linecast(playerCharacter.transform.position, castDirection,wall);

        player.jumping(hit);

        if (hit)
        {

            player.bigBack.transform.rotation = Quaternion.identity;
            player.bigFront.transform.rotation = Quaternion.identity;

            if (numberTimer <= 0 || numJumps < 3)
            {
                jumpCanvas.SetActive(false);


            }

            numJumps = 3;


        }
        else
        {

            player.bigBack.transform.up = rigidbodyModule.linearVelocity;
            player.bigFront.transform.up = rigidbodyModule.linearVelocity;


        }

        //Jumping control
        if (Input.GetMouseButtonDown(0))
        {
            if (numJumps > 0)
            {
                rigidbodyModule.AddForce((mousePosition - (Vector2)playerCharacter.transform.position) * force);
                numberTimer = 1;
                jumpCanvas.SetActive(true);
                //add logic that if the first jump is on the floor, numjumps doesnt change.
                if (hit == false)
                {
                    numJumps -= 1;

                }


            }

        }

        if (numberTimer >= 0)
        {
            numberTimer -= Time.deltaTime;
        }
        //Screen control
        if (coins >= 3)
        {
            gameOverText.text = "You win!";
            coins = 0;

            GameOver();


        }

        if (timeleft >= 0)
        {
            timeleft -= Time.deltaTime;
            if (timeleft <= 0)
            {
                if (lives > 0)
                {
                    RespawnPlayer();
                    getReadyScreen.SetActive(false);


                }
                else
                {
                    introPage.SetActive(true);
                    gameOverScreen.SetActive(false);
                    DestroyImmediate(LevelCloneb);
                    DestroyImmediate(LevelClonef);

                }




            }

        }



    }


}
