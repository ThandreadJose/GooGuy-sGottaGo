using UnityEngine;
using UnityEngine.InputSystem.Users;
using System;

public class BasePU : Power
{
    public override void PowerUp()
    {
        Debug.Log("omg wowza");
        state = "DONE";
    }
}
