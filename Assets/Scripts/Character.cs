using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] private Vector2 mousePosition;

    public float moveSpeed;
    public bool isDead;

    public Rigidbody2D rigidbodyModule;
    public GameManager gameManager;

    public GameObject bigBack;
    public GameObject bigFront;
    public GameObject smallBack;
    public GameObject smallFront;
    public GameObject leftEye;
    public GameObject rightEye;
    public GameObject leftPupil;
    public GameObject rightPupil;
    public SpriteRenderer backSprite;
    public SpriteRenderer frontSprite;

    public Sprite frontJump;
    public Sprite backJump;

    public Sprite frontNormal;
    public Sprite backNormal;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void jumping(bool airborne)
    {
        if (airborne)
        {
            frontSprite.sprite = frontNormal;
            backSprite.sprite = backNormal;
        }
        else
        {

            frontSprite.sprite = frontJump;
            backSprite.sprite = backJump;

        }

    }

    // Update is called once per frame
    void Update()
    {



    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            //kill the character
            gameManager.lives -= 1;
            gameManager.Died();

        }

        if (other.CompareTag("Boost"))
        {
            //Make a coin/boost
            gameManager.GetCoin();
        }

        if (other.CompareTag("Checkpoint"))
        {

            gameManager.SetSpawnPoint(other.transform.position);
        }

    }
    //plan is, create a UI that has GameOver on it, make it so that the player dies as it tocuhes the trap. then have a counter work on the coins. and blamo done for now.


}
