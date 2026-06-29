using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Boosts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject boost;
    public MonoScript power;
    private Type baseType = typeof(Power);
    private Type type;
    private Power newPower;

    //public PowerUp power;

    void Start()
    {
        type = power.GetClass();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            GameObject go = other.gameObject;
            Character character = go.GetComponentInParent<Character>();
            if (baseType.IsAssignableFrom(type))
            {

                newPower = (Power)ScriptableObject.CreateInstance(type);

                if (newPower) { Debug.Log(newPower.GetType().ToString()); }

                character.powerUpController.addPower(newPower);

                boost.SetActive(false);
            }
            else
            {
                Debug.Log(power.GetType().ToString());
            }
        }

            
    }
}
