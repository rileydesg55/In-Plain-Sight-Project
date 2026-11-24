using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskDoing : MonoBehaviour
{
    public PlayerMovement Player;
    public int percentage;
    public bool doingTask;
    public TMP_Text TaskPercentage;
    public TMP_Text tasksToDo;
    private bool timerUp;
    public bool taskDone;
    public TaskAmountTick taskAmount;
    private bool tickedDown;
    public GameObject terminal;
    public GameObject bag;
    // Start is called before the first frame update
    void Start()
    {
        tickedDown = false;
        percentage = 0;
        timerUp = true;
        taskDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        terminal = Player.getTerminal();
        /*Debug.Log("taskdoing is " + Player.doingTask);
        Debug.Log("timerup is " + timerUp);
        Debug.Log("taskDone is " + taskDone);*/

        //Player has to be on the task AND cooldown between percentage over for it to tick up
        if (doingTask && timerUp && !taskDone)
        {
            increasePercentage();
        }

        //if player isnt on the termial the text just goes away
        if (!doingTask)
        {
            TaskPercentage.SetText("");
            taskDone = false;
        }
        //if player is on the termial the text updates percentage
        if (doingTask && !taskDone)
        {
            //TaskPercentage.SetText(percentage + "%");
        }
        //color is white when not complete
        if (!taskDone)
        {
            TaskPercentage.color = Color.white;
        }
        //changes color to signify complete task
        if (taskDone && doingTask)
        {
            TaskPercentage.color = Color.green;
            TaskPercentage.SetText("Complete!");
            StartCoroutine(tickDownTask());
        }
    }

    //makes it so the percentage doesnt go up instantly it takes some time
    IEnumerator PercentageIncrease()
    {
        taskDone = false;
        timerUp = false;
        percentage++;
        yield return new WaitForSecondsRealtime(0.1f);
        //Debug.Log(percentage);
    }

    void increasePercentage()
    {
        if(SceneHandle.previousTask == 6)
        {
            taskDone = PlinkoBall.completedTask;
            Debug.Log("Task is " + taskDone);
        } else if(SceneHandle.previousTask == 7)
        {
            taskDone = TaskCheck.taskDone;
            Debug.Log("Task is " + taskDone);
        } else if(SceneHandle.previousTask == 8)
        {
            taskDone = Touching.done;
            Debug.Log("Task is " + taskDone);
        } else if(SceneHandle.previousTask == 9)
        {
            taskDone = Tap.completedTask;
            Debug.Log("Task is " + taskDone);
        } else
        {
            
        }
        //for as long as the percentage is not full it ticks up, and when full it says complete
        if (!taskDone || percentage < 1)
        {
            StartCoroutine(PercentageIncrease());
            timerUp = true;
            SceneHandle.sceneSwitch(false, SceneHandle.tmission, true);
            
        } else if(taskDone)
        {
            Debug.Log("done");
            //taskDone = true;
            taskDone = true;
            terminal.GetComponent<SpriteRenderer>().color = Color.clear;
            bag.SetActive(false);
        }
    }

    IEnumerator tickDownTask()
    {
        if (!tickedDown)
        {
            TaskAmountTick.totalTasks--;
            tickedDown = true;
            yield return new WaitForSecondsRealtime(5f);
        }
       
    }
}
