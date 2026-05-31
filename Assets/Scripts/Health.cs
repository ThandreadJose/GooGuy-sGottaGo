using UnityEngine;

public class Health : MonoBehaviour
{

    public float healthPoints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void IncreaseHealth()
    {
        healthPoints += 1;
    }

    // Update is called once per frame
    public void DecreaseHealth()
    {
        healthPoints -= 1;
    }
}
