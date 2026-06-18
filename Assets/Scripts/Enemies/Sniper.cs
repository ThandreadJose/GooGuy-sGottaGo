using UnityEngine;

public class Sniper : Enemy
{
    public GameObject missile;

    public override void Reload()
    {
        base.Reload();

        if (missile != null)
        { missile.SetActive(true); }
    }

    public override void shootBullet()
    {
        base.shootBullet();

        if (missile != null)
        {
            missile.SetActive(false);
        }
    }

}
