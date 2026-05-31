using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{

    //hte plan is to make 2 separate bullets, one is the sniper bullet that bounces, the other will be the machine gun bullets that are slow and dont bounce

    public GameObject bullet;
    public Rigidbody2D rb;
    public Collider2D bulletCollider;
    public Sprite[] sprites;
    public Sprite[] spritesBack;
    public SpriteRenderer activeSprite;
    public SpriteRenderer activeBack;
    private int spriteStep;
    private float timer;

    public float speed;
    public int health;


    // Start is called once before the first execution of Update after the MonoBehaviour is created




    void Start()
    {
        bullet = this.gameObject;
        rb.AddForce((bullet.transform.up * speed));
        spriteStep = 0;
        timer = 0.1f;
        
    }

    public void ShuffleSprite()
    {
        if (sprites.Length > 0)
        {
            spriteStep += 1;
            timer = 0.1f;
            if (spriteStep > sprites.Length)
            {
                spriteStep = 0;
            }

            activeSprite.sprite = sprites[spriteStep];
            if (activeBack  != null)
            {
                activeBack.sprite = spritesBack[spriteStep];
            }
            
        }


    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        bullet.transform.up = rb.linearVelocity;
        if (timer < 0)
        {

            ShuffleSprite();
        }

        if (health <= 0)
        {
            DestroyImmediate(bullet);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            health -= 1;

        }
        if (other.CompareTag("Player"))
        {
            health -= 1;
            GameObject player = other.gameObject;
            GameObject parentgo = player.transform.parent.gameObject;

            Character character = parentgo.GetComponent<Character>();


                character.gameManager.lives -= 1;
                character.gameManager.Died();

        }
    }
}
