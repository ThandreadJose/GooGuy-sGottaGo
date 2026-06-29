using System;
using UnityEngine;

[System.Serializable]
public class Power : ScriptableObject
{
    //NONE = new, APPLIED = stats are changed, ACTIVE = currently in use, OFF = ability has been switched, DONE = ability has run its course.
    public string state = "NONE";
    //type will be to help navigate how to separate 
    protected string type = "NONE";
    public int maxJumps = 0;
    public int force = 100;
    public Action ability;
    protected string label = "Power";
    public PowerUpController powerUpController;

    public virtual void ActivatePower(Action newAbility)
    {
        ability = newAbility;
    }
    //This is called as the powers change, this is to take away and buffs/physics changes a power may do (like the spike power when stuck)
    public virtual void ResetStats() 
    {
        ability = null;
    
    }

    public virtual void ApplyOwner(PowerUpController newPowerUpController)
    {
        powerUpController = newPowerUpController;
    }
    //This is used to make sure that if I want any stats changed itll be called here
    public virtual void ApplyStats(GameManager gm)
    {
        gm.maxJumps = maxJumps;
        gm.force = force;
    }
    //If there are stats that should change, have this called the power itself with the stat changes. reason for this is just to make sure that all the information is properly initialized and travels to the right place.
    public virtual void InitializePower()
    {
        state = "APPLIED";
    }

    //PowerUp will be the ability that the power has, if the power doesnt have an active ability and only has stat changes, this will be considered as DONE as soon as it spawns
    public virtual void PowerUp()
    {

    }

}
