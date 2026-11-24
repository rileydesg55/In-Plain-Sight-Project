using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandle : MonoBehaviour
{
    public bool inTask;
    public static int tmission;


    public static GameObject rileyScene;
    public static GameObject mainScene;
    public static GameObject plinkoScene;
    public static GameObject wireScene;
    public static GameObject mazeScene;
    public static GameObject cardScene;
    public static GameObject selectScene;
    public static GameObject titleScene;

    public static PlayerMovement player;
    public static PlinkoBall plinko;

    public static int currentTask;
    public static int previousTask;
    public bool doneLoading;
    // Start is called before the first frame update
    void Start()
    {

        //SceneManager.LoadScene(0, LoadSceneMode.Additive);

        tmission = 0;
    }

    // Update is called once per frame
    void Update()
    {
        inTask = player.doingTask;
        Debug.Log(plinkoScene + "exists");
        Debug.Log(mainScene + "exists");
        /*doneLoading = AsyncOperation.isDone;
        if (*/

    }

    public static void sceneSwitch(bool inTask, int mission, bool forTask)
    {
        tmission = mission;
        int randomNum = Random.Range(6, 9);
        Debug.Log(currentTask);
        Debug.Log(previousTask);
        Debug.Log(tmission);
        if (forTask)
        {
            Debug.Log("loaded");
            if (inTask)
            {
                
                if(previousTask == 6)
                {
                    SceneManager.UnloadSceneAsync(6);
                } else if(previousTask == 7)
                {
                    SceneManager.UnloadSceneAsync(7);
                } else if(previousTask == 8)
                {
                    SceneManager.UnloadSceneAsync(8);
                } else if(previousTask == 9)
                {
                    SceneManager.UnloadSceneAsync(9);
                }
                mainScene.SetActive(true);
                plinkoScene.SetActive(false);
                wireScene.SetActive(false);
                cardScene.SetActive(false);
            }
            if (!inTask)
            {
                SceneManager.LoadScene(currentTask, LoadSceneMode.Additive);

                mainScene.SetActive(false);
            }
            previousTask = currentTask;
            currentTask = randomNum;
        } else if (!forTask)
        {
            
            if (mission == 0)
            {
                //SceneManager.LoadScene()//
                titleScene.SetActive(true);
                tmission = 0;
            }
            if(mission == 1)
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                titleScene.SetActive(false);
                selectScene.SetActive(false);
                SceneManager.UnloadSceneAsync(0);
                tmission = 1;

            }
            if(mission == 2)
            {
                SceneManager.LoadScene(2, LoadSceneMode.Single);
                selectScene.SetActive(false);
                mainScene.SetActive(false);
                tmission = 2;
                Debug.Log(tmission);
            }
            if (mission == 3)
            {
                SceneManager.LoadScene(3, LoadSceneMode.Single);
                selectScene.SetActive(false);
                mainScene.SetActive(false);
                tmission = 3;
                Debug.Log(tmission);
            }
            if (mission == 4)
            {
                SceneManager.LoadScene(4, LoadSceneMode.Single);
                selectScene.SetActive(false);
                mainScene.SetActive(false);
                tmission = 4;
                Debug.Log(tmission);
            }
            if (mission == 5)
            {
                SceneManager.LoadScene(5, LoadSceneMode.Single);
                selectScene.SetActive(false);
                mainScene.SetActive(false);
                tmission = 5;
                Debug.Log(tmission);
            }
        }

  
    }

}
