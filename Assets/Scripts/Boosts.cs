using UnityEngine;

public class Boosts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject boost;
    public Power power;
    //public PowerUp power;

    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        GameManager gameManager = other.GetComponent<GameManager>();
        //gameManager.ChangePower(power);
        boost.SetActive(false);
    }
}
