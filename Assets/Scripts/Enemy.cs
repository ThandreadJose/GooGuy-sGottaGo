using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class Enemy : MonoBehaviour
{

    public GameObject enemyObject;
    private bool lockOn = false;
    private Collider2D colliderPlayer;
    public GameObject bullet;
    private GameObject bulletClone;
    [SerializeField]protected float cooldown;
    protected float timer;
    protected bool reloaded = true;
    private float betweenShots;
    public int numberOfShots;
    protected int maxShots;
    public Rigidbody2D rb;
    public GameObject body;

    //public GameObject missile;



    //This will control what is being aimed
    public GameObject lead;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = cooldown;
        betweenShots = 0.5f;
        maxShots = numberOfShots;

        
    }

    public virtual void lockedIn(Collider2D player)
    {

        lead.transform.up = player.transform.position - enemyObject.transform.position;
    }

    public virtual void lockedOut()
    {
        float speed = 2f;


        Vector3 nmeObj = enemyObject.transform.rotation.eulerAngles;

        if (nmeObj.z > 180)
        {
            nmeObj.z = nmeObj.z - speed;
            if (nmeObj.z < 180)
            {
                nmeObj.z = 180;
            }
        }
        if (nmeObj.z < 180)
        {
            nmeObj.z = nmeObj.z + speed;
            if (nmeObj.z > 180)
            {
                nmeObj.z = 180;
            }

        }

        lead.transform.rotation = Quaternion.Euler(0f, 0f, nmeObj.z);
    }

    public virtual void Reload()
    {
        reloaded = true;
    }


    public virtual void shootBullet()
    {
            
            bulletClone = Instantiate(bullet, enemyObject.transform.position, enemyObject.transform.rotation);
            bulletClone.transform.up = lead.transform.up;
            numberOfShots -= 1;

            if (numberOfShots <= 0)
            {
            reloaded = false;
            timer = cooldown;
                numberOfShots = maxShots;


            }

    }

    // Update is called once per frame
    void Update()
    {
        body.transform.rotation = Quaternion.identity;
        if (reloaded == false)
        {
            timer -= Time.deltaTime;
        }
        if (lockOn == true)
        {
            lockedIn(colliderPlayer);
            if (reloaded == true)
            {

                betweenShots -= Time.deltaTime;
                if (betweenShots < 0)
                {
                    betweenShots = 0.5f;
                    shootBullet();
                    
                }
            }
        }

        if (lockOn == false)
        {
            lockedOut();

        }
        if (timer <= 1)
        {
            Reload();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lockOn = true;
            colliderPlayer = other;
            


        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lockOn = false;
            
            timer = cooldown;


        }
    }
}
