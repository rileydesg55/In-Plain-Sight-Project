using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLevelBehavior : MonoBehaviour
{

    public GameObject thirdLevel;
    public GameObject terminal;
    public GameObject exit;
    public Rigidbody2D player;
    public PlayerMovement playerMove;

    // Start is called before the first frame update
    void Start()
    {
        SceneHandle.mainScene = thirdLevel;
        TaskAmountTick.totalTasks = 4;
        SceneHandle.currentTask = Random.Range(6, 9);
        playerMove.energy = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        SceneHandle.tmission = 3;


        if (TaskAmountTick.totalTasks < 1)
        {
            openExit();
        }
    }


    public void openExit()
    {
        exit.SetActive(true);
    }
}
