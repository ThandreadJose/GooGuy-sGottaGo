using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite Neutral;
    public Sprite Captured;
    public SpriteRenderer currentSprite;
    public bool captureState;
    public GameManager gameManager;
    public GameObject flag;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        captureState = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (captureState == true)
        {
            currentSprite.sprite = Captured;
        }
        else
        {
            currentSprite.sprite = Neutral;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            captureState = true;

        }
    }
}
