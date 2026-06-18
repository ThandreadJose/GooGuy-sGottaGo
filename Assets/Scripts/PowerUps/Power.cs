using System;
using UnityEngine;

public class Power
{

    protected string powerType = "NONE";
    protected string state = "NONE";
    protected bool active = false;
    protected int maxJumps = 1;
    protected int force = 100;
    protected Action ability;

    public virtual void activatePower(int mJump, int force, Action ability)
    {

    }
}
