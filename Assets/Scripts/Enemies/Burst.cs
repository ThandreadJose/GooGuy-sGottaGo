using UnityEngine;

public class Burst : Enemy
{
    public GameObject arm;

    public override void lockedOut()
    {
        base.lockedOut();
        if (arm != null)
        {
            if (arm.transform.localPosition.y >= 0.5f)
            {
                float dis = arm.transform.localPosition.y;
                dis -= 0.1f;
                arm.transform.localPosition = new Vector3(0f, dis, 1);

            }
        }
    }

    public override void lockedIn(Collider2D player)
    {
        base.lockedIn(player);

        if (arm != null)
        {
            if (arm.transform.localPosition.y <= 0.8f)
            {
                float dis = arm.transform.localPosition.y;
                dis += 0.1f;
                arm.transform.localPosition = new Vector2(0f, dis);
            }
        }
    }
}
