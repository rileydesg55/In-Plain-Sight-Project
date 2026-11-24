using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLevelBehavior : MonoBehaviour
{
    public GameObject firstLevel;
    public GameObject terminal;
    public GameObject obstacles;
    public Rigidbody2D player;
    public PlayerMovement playerScript;
    public FinishLevel finishTrigger;
    public bool exitUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        exitUnlocked = true;
        TaskAmountTick.totalTasks = 2;
        SceneHandle.mainScene = firstLevel;
        Instantiate(terminal, new Vector2(-1.95f, -3.26f), Quaternion.identity, firstLevel.transform);
        player.position = new Vector2(3.9f, 0.75f);
        SceneHandle.currentTask = Random.Range(6, 9);
        playerScript.energy = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        SceneHandle.tmission = 1;



        if (TaskAmountTick.totalTasks < 1)
        {
            openExit();
        }

        
    }

    public void openExit()
    {
        obstacles.SetActive(false);
        exitUnlocked = true;
    }
}
