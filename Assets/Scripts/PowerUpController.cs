using UnityEngine;
using UnityEngine.InputSystem.Controls;
using System.Collections.Generic;

public class PowerUpController : MonoBehaviour

// So the idea is that this should be a base component that has the function of what the power actually does. I think that having this change the stats of the player and add their own proper functions would be kind of epic. this should not have the update function but should have a way to grab the info from the important variables from game controller and have it take priority. 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected GameManager gameManager;
    protected string state = "NONE";
    protected bool active = false;
    private Power activePower;
    private List<Power> powerList;

    //this will be called first to change stats like number of jumps or hp (idk yet)
    public virtual void updateStats(int maxJ, int uForce)
    {
        gameManager.maxJumps = maxJ;
        gameManager.force = uForce;
        
    }


    //this will add the power to the list of powers.
    public void addPower(Power newPower)
    {
        activePower = newPower;
        powerList.Add(newPower);
    }



    //this will update the state of the power, like controlling when it is doing its thing and when it is used up.
    public virtual void newState(string newstate)
    {
        state = newstate;
        checkState(state);
    }
    public virtual void checkState(string current)
    {
        if (current == "ACTIVE")
        {
            active = true;
        }
        if (current == "DEACTIVE")
        {
            active = false;
        }

    }

    //public virtual void Activate()
    //{
    //    updateStats();
    //    newState("ACTIVE");

    //}
}
