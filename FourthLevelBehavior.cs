using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthLevelBehavior : MonoBehaviour
{
    public GameObject fourthLevel;
    public PlayerMovement playerMove;
    public SusBar susBar;
    // Start is called before the first frame update
    void Start()
    {
        SceneHandle.mainScene = fourthLevel;
        TaskAmountTick.totalTasks = 0;
        SceneHandle.currentTask = Random.Range(6, 9);
        playerMove.energy = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        SceneHandle.tmission = 4;
        susBar.sus = 99;
    }
}
