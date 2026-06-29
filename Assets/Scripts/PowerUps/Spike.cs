using System;
using Unity.VisualScripting;
using UnityEngine;


//This will be to stik to the wall
public class Spike : Power
{
    public GameObject goPrefab;
    private Collider2D collider;
    private bool stuck = false;
    private GameObject go;

    public override void ActivatePower(Action newAbility)
    {
        
        GameObject goPrefab = Resources.Load<GameObject>("Prefabs/Powers/SpikeCollider");
        go = Instantiate(goPrefab, powerUpController.character.transform);

        base.ActivatePower(newAbility);
    }

    public override void ResetStats()
    {
        base.ResetStats();
        Character character = powerUpController.character;
        character.rigidbodyModule.gravityScale = 1.0f;
    }

    public override void PowerUp()
    {
        collider = go.GetComponent<Collider2D>();
        LayerMask wall = LayerMask.GetMask("Obstacles");
        Character character = powerUpController.character;

        Debug.Log(state);
        if (state == "ACTIVE")
        {
            if (stuck == false) 
            {
                

                if (character.airborne == true)
                {
                    

                    if (collider.IsTouchingLayers(wall))
                    {
                        
                        character.rigidbodyModule.angularVelocity = 0;
                        character.rigidbodyModule.linearVelocity = Vector2.zero;
                        character.rigidbodyModule.gravityScale = 0;
                        character.rigidbodyModule.mass = 0;
                        stuck = true;
                        powerUpController.gameManager.numJumps = 1;
                        state = "DONE";

                    }

                }
            }
            if (stuck == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    character.rigidbodyModule.gravityScale = 1;
                    character.rigidbodyModule.mass = 1;
                    stuck = false;

                }
            }

        }
        if (state == "DONE")
        {
                if (Input.GetMouseButtonDown(0))
                {
                    character.rigidbodyModule.gravityScale = 1;
                    character.rigidbodyModule.mass = 1;
                    stuck = false;

                }
        }
           
    }


}
