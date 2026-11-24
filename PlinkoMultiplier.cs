using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlinkoMultiplier : MonoBehaviour
{
    public PlinkoBall moneyStuff;
    public Rigidbody2D ball;
    int wager;
    double money;
    double payout;
    // Start is called before the first frame update
    void Start()
    {
        payout = 0;
    }

    // Update is called once per frame
    void Update()
    {
        wager = moneyStuff.wager;
        money = moneyStuff.money;

        if(ball.position.y < -10 && ball != null)
        {
/*            StartCoroutine(ballCountdowntoExtinction());
            Destroy(ball);*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("500x"))
        {
            payout = wager * 100;
        } else if (collision.CompareTag("100x"))
        {
            payout = wager * 50;
        } else if (collision.CompareTag("25x"))
        {
            payout = wager * 25;
        } else if (collision.CompareTag("2x"))
        {
            payout = wager * 2;
        } else if (collision.CompareTag("0.5x"))
        {
            payout = wager * 0.5f;
        } else if (collision.CompareTag("0.2x"))
        {
            payout = wager * 0.2f;
        }
        moneyStuff.money = money + payout;
        
    }

    IEnumerator ballCountdowntoExtinction()
    {
        yield return new WaitForSecondsRealtime(20f);
    }
}
