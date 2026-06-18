using UnityEngine;

public class Melee : Enemy
{
    private SpriteRenderer bSprite;
    private int side; // 0 = left 1 = right


    public override void lockedIn(Collider2D player)
    {
        bSprite = body.GetComponent<SpriteRenderer>();

        if (player.transform.position.x > enemyObject.transform.position.x)
        {
            bSprite.flipX = true;
            side = 0;
            lead.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (player.transform.position.x < enemyObject.transform.position.x)
        {
            bSprite.flipX = false;
            side = 1;
            lead.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }


    public override void shootBullet()
    {
        reloaded = false;
        if (side == 0)
            {
                lead.transform.rotation = Quaternion.Euler(0f, 0f, -45);
            }
            if (side == 1)
            {
                lead.transform.rotation = Quaternion.Euler(0f, 0f, 45);
            }
            float speed = 2f;
            rb.AddForce(enemyObject.transform.up * speed, ForceMode2D.Impulse);
            numberOfShots -= 1;
            if (numberOfShots <= 0)
            {
                timer = cooldown;
                numberOfShots = maxShots;


            }
            lead.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
