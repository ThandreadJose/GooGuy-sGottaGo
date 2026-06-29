using System;
using Unity.VisualScripting;
using UnityEngine;
public class TripleJump : Power
{

    public override void InitializePower()
    {
        base.InitializePower();
        maxJumps = 5;
        force = 100;
    }

    public override void PowerUp()
    {
        Debug.Log("max jumps");
        state = "DONE";
    }

}
