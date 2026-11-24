using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{

    bool isAttacking;
    bool onCooldown;
    float meleeMomentum;
    string direction;
    bool crawling;
    bool locked;

    public Rigidbody2D player;
    public Rigidbody2D range;
    public PlayerMovement playerMove;
    public SusBar susMeter;

    float playerX;
    float playerY;
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        onCooldown = false;
        //melee momentum is how much player lunges when doing melee attack
        meleeMomentum = 150f;
        crawling = false;
    }

    // Update is called once per frame
    void Update()
    {



        //eventually make sure no other weapon equipped before doing melee
        //the 0 parameter is just left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //gets different player conditions
            direction = playerMove.getDirection();
            crawling = playerMove.getCrawling();
            locked = playerMove.getMovementLock();
            MeleeAttack();
        }
        if (isAttacking == true)
        {

            //based on which way the player is facing the melee attack lunges them slightly in that direction
            if(direction == "Up")
            {
                player.AddForce(new Vector2(0f, meleeMomentum));
            } else if(direction == "Left")
            {
                player.AddForce(new Vector2(-meleeMomentum, 0f));
            } else if(direction == "Down")
            {
                player.AddForce(new Vector2(0f, -meleeMomentum));
            } else if(direction == "Right")
            {
                player.AddForce(new Vector2(meleeMomentum, 0f));
            }
            StartCoroutine(MeleeCooldown());
        }

        playerX = player.transform.position.x;
        playerY = player.transform.position.y;

        //the player has a melee range in which any enemy in that range is considered hit that rotates around the player based on his direction
        if(direction == "Right")
        {
            range.transform.position = new Vector2(playerX + 0.75f, playerY);
        } else if(direction == "Down")
        {
            range.transform.position = new Vector2(playerX, playerY + -1.25f);
        } else if(direction == "Left")
        {
            range.transform.position = new Vector2(playerX + -0.75f, playerY);
        } else if(direction == "Up")
        {
            range.transform.position = new Vector2(playerX, playerY + 0.75f);
        }
    }


    public void MeleeAttack()
    {
        //checks player is standing and able to melee, then attacking is true
        if (!isAttacking && !onCooldown && !crawling && !locked)
        {
            isAttacking = true;
            //Debug.Log("Clicked");
            susMeter.sus = susMeter.sus + 5;
            if(susMeter.sus > 100)
            {
                susMeter.sus = 100;
            }
            playerMove.energy = playerMove.energy - 20;
        }
    }
    IEnumerator MeleeCooldown()
    {
        //puts melee on a cooldown as to not constantly attack
        isAttacking = false;
        onCooldown = true;
        yield return new WaitForSecondsRealtime(0.5f);
        onCooldown = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //checks if melee attack hits enemy. eventually need to add enemy invulnerability frames
        if(collision.gameObject.CompareTag("Enemy") && onCooldown)
        {
            Debug.Log("Hit the enemy");
        }
    }

    public bool getAttacking()
    {
        return isAttacking;
    }
}
