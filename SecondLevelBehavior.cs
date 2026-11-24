using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondLevelBehavior : MonoBehaviour
{
    public GameObject secondLevel;
    public GameObject terminal;
    public GameObject exit;
    public Rigidbody2D player;
    public PlayerMovement playerMove;

    // Start is called before the first frame update
    void Start()
    {
        SceneHandle.mainScene = secondLevel;
        TaskAmountTick.totalTasks = 3;
        Instantiate(terminal, new Vector2(8.8f, -1.5f), Quaternion.identity, secondLevel.transform);
        SceneHandle.currentTask = Random.Range(6, 9);
        playerMove.energy = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        SceneHandle.tmission = 2;


        if (TaskAmountTick.totalTasks < 1)
        {
            openExit();
        }


    }

    public void openExit()
    {
        exit.SetActive(false);
    }
}
