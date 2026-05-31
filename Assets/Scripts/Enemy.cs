using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject enemyObject;
    public Collider2D colliderNME;
    private bool lockOn = false;
    private Collider2D colliderPlayer;
    public GameObject bullet;
    private GameObject bulletClone;
    public float cooldown;
    private float timer;
    private float betweenShots;
    public int numberOfShots;
    private int maxShots;
    private bool ranged;
    public Rigidbody2D rb;
    public GameObject body;
    public GameObject arm;
    public GameObject missile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = cooldown;
        betweenShots = 0.5f;
        maxShots = numberOfShots;
        ranged = true;

        if (bullet == null)
        {
            ranged = false;
        }
    }

    public void lockedIn(Collider2D player)
    {
        if (ranged == true)
        {
            enemyObject.transform.up = player.transform.position - enemyObject.transform.position;
        }
        else
        {
            if (player.transform.position.x > enemyObject.transform.position.x)
            {
                enemyObject.transform.rotation = Quaternion.identity;

                enemyObject.transform.Rotate(0,0,-45);

            }
            if (player.transform.position.x < enemyObject.transform.position.x)
            {
                enemyObject.transform.rotation = Quaternion.identity;

                enemyObject.transform.Rotate(0, 0, 45);

            }
        }

    }

    public void shootBullet()
    {
        if (ranged == true)
        {
            bulletClone = Instantiate(bullet, enemyObject.transform.position, enemyObject.transform.rotation);
            bulletClone.transform.up = enemyObject.transform.up;
            numberOfShots -= 1;
            if (missile != null)
            {
                missile.SetActive(false);
            }
            if (numberOfShots <= 0)
            {
                timer = cooldown;
                numberOfShots = maxShots;


            }
        }
        if (!ranged)
        {

            float speed = 2f;
            rb.AddForce(enemyObject.transform.up * speed,ForceMode2D.Impulse);
            numberOfShots -= 1;
            if (numberOfShots <= 0)
            {
                timer = cooldown;
                numberOfShots = maxShots;


            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        body.transform.rotation = Quaternion.identity;

        if (lockOn == true)
        {
            lockedIn(colliderPlayer);
            timer -= Time.deltaTime;
            if (arm != null)
            {
                if (arm.transform.localPosition.y <= 0.8f)
                {
                    float dis = arm.transform.localPosition.y;
                    dis += 0.1f;
                    arm.transform.localPosition = new Vector2(0f, dis);
                }
            }
            if (timer <= 0)
            {
                if (missile != null)
                { missile.SetActive(true); }
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
            float speed = 2f;
            if (arm != null)
            {
                if (arm.transform.localPosition.y >= 0.5f)
                {
                    float dis = arm.transform.localPosition.y;
                    dis -= 0.1f;
                    arm.transform.localPosition = new Vector3(0f, dis, 1);

                }
            }

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

            enemyObject.transform.rotation = Quaternion.Euler(0f, 0f, nmeObj.z);
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
