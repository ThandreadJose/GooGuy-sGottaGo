using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using System.Collections.Generic;

public class PowerUpController : MonoBehaviour

// So the idea is that this should be a base component that has the function of what the power actually does. I think that having this change the stats of the player and add their own proper functions would be kind of epic. this should not have the update function but should have a way to grab the info from the important variables from game controller and have it take priority. 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameManager gameManager;
    public Character character;
    public Power activePower;
    public List<Power> powerList;

    //this will be called first to change stats like number of jumps or hp (idk yet)



    //this will add the power to the list of powers.
    public void addPower(Power newPower)
    {
        changePower(newPower);
        powerList.Add(newPower);
        newPower.ApplyOwner(this);
    }

    public void changePower(Power newPower)
    {
        if (activePower != null && activePower != newPower)
        {
            if (character.airborne == true)
            {
                activePower.state = "USED";
            }
        }

        activePower = newPower;
    }

    private void Update()
    {
        if (activePower != null)
        {

            if (activePower.state == "NONE")
            {

                activePower.InitializePower();
                activePower.ApplyStats(gameManager);

                
            }
            if (activePower.state == "APPLIED")
            {
                activePower.ActivatePower(activePower.PowerUp);
                activePower.state = "ACTIVE";
            }
            if (activePower.state == "ACTIVE")
            {
                activePower.ability();
            }
            if (activePower.state == "DONE")
            {
                activePower.ability();
            }

            if (character.airborne == false)
            {
                if (activePower.state == "DONE")
                {
                    activePower.state = "ACTIVE";
                }
            }
        }

    }



    //this will update the state of the power, like controlling when it is doing its thing and when it is used up.

    //public virtual void Activate()
    //{
    //    updateStats();
    //    newState("ACTIVE");

    //}
}
