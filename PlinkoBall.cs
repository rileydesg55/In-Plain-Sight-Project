using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlinkoBall : MonoBehaviour
{
    public Rigidbody2D ball;
    public Rigidbody2D freeBall;
    public int numBalls;
    public double money;
    public int wager;
    int ballWager;
    Vector2 ballMoveForce;
    bool dropped;
    bool canWager;
    public TMP_Text moneyText;
    public TMP_Text wagerText;
    public static bool completedTask;

    public GameObject plinkoScene;

    // Start is called before the first frame update
    void Start()
    {
        SceneHandle.plinkoScene = plinkoScene;
        SceneHandle.plinko = ball.GetComponent<PlinkoBall>();

        completedTask = false;
        canWager = true;
        money = 50;
        wager = 5;
/*        numBalls = 20;*/
        //ball.gravityScale = 1.15f;
        ballMoveForce = new Vector2(0.5f, 0f);
        dropped = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*ballText.SetText("You have " + numBalls + " balls");*/
        moneyText.SetText("You have $" + (int)money);
        wagerText.SetText("Current bet is $" + wager);
       // Debug.Log(canWager);
        if (!dropped)
        {
            ball.gravityScale = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(spawnBall());
            }

            if (Input.GetKey(KeyCode.A))
            {
                ball.AddForce(-ballMoveForce);
            }
            if (Input.GetKey(KeyCode.D))
            {
                ball.AddForce(ballMoveForce);
            }
        }
        if (freeBall.position.y > -1)
        {
            canWager = false;
        } else
        {
            canWager = true;
        }
        if (canWager)
        {
            if (wager > money)
            {
                //wager = (int)money;
            }
        }

        if(money > 1000)
        {
            completedTask = true;
        }
        if (completedTask)
        {
            SceneHandle.sceneSwitch(true, SceneHandle.tmission, true);
        }

        if(money < 1)
        {
            SceneHandle.sceneSwitch(true, SceneHandle.tmission, true);
        }
    }

    IEnumerator spawnBall()
    {
        if(money > 0 && wager < money)
        {
            freeBall = Instantiate(ball, new Vector3(ball.position.x, ball.position.y -0.5f, ball.rotation), Quaternion.identity, plinkoScene.transform);
            freeBall.gravityScale = 0.8f;
            money = money - wager;
        }
        yield return new WaitForSecondsRealtime(0.25f);
    }

    /*public void buyBalls()
    {
        if(money > 10)
        {
            numBalls = numBalls + 5;
            money = money - 10;
        }
    }*/

    public void increaseBet()
    {
        if (canWager)
        {
            if(wager != money || wager !> money)
            {
                wager++;
            }
        }
    }

    public void decreaseBet()
    {
        if (canWager)
        {
            if(wager > 1)
            {
                wager--;
            }
        }
    }
    public void AllIn()
    {
        if (canWager)
        {
            wager = (int)money;
        }
    }

    public void Reset()
    {
        SceneHandle.plinko = ball.GetComponent<PlinkoBall>();

        completedTask = false;
        canWager = true;
        money = 50;
        wager = 5;
        /*        numBalls = 20;*/
        ball.gravityScale = 1.15f;
        dropped = false;
    }

}
