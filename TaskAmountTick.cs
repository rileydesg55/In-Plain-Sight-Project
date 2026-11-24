using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskAmountTick : MonoBehaviour
{
    public static int totalTasks;
    public TMP_Text tasksToDo;
    public string firstTask;
    public string secondTask;
    public string thirdTask;
    public string fourthTask;
    public string fifthTask;
    // Start is called before the first frame update
    void Start()
    {
        firstTask = "";
        secondTask = "";
        thirdTask = "";
        fourthTask = "";
        fifthTask = "";


    }

    // Update is called once per frame
    void Update()
    {
        if (SceneHandle.tmission == 1)
        {
            firstTask = "-Check your computer";
            secondTask = "-Find the key in your kitchen";
            thirdTask = "-Escape your home!";
        }
        else if (SceneHandle.tmission == 2)
        {
            firstTask = "-Get Documents from the meeting room";
            secondTask = "-Find information in the boss's room";
            thirdTask = "-Speak to your informant";
            fourthTask = "-Head to the train station";
        } else if (SceneHandle.tmission == 3)
        {
            firstTask = "-Ask witness what he knows";
            secondTask = "-Ask witness for information";
            thirdTask = "-Pick up luggage";
            fourthTask = "-Ask informant who it was";
            fifthTask = "-Get on the north train";
        }

        tasksToDo.SetText(totalTasks.ToString());
        if(SceneHandle.tmission == 1)
        {
            if(totalTasks > 0)
            {
                tasksToDo.SetText(totalTasks.ToString() + " " + firstTask + " " + secondTask);
            } else if (totalTasks < 1)
            {
                tasksToDo.SetText(thirdTask);
            }
        } else if(SceneHandle.tmission == 2)
        {
            if(totalTasks > 0)
            {
                tasksToDo.SetText(totalTasks.ToString() + " " + firstTask + " " + secondTask + " " + thirdTask);
            } else if (totalTasks < 1)
            {
                tasksToDo.SetText(fourthTask);
            }
        } else if(SceneHandle.tmission == 3)
        {
            if(totalTasks > 0)
            {
                tasksToDo.SetText(totalTasks.ToString() + " " + firstTask + " " + secondTask + " " + thirdTask + " " + fourthTask);
            } else if (totalTasks < 1)
            {
                tasksToDo.SetText(fifthTask);
            }
        }
    }
}
