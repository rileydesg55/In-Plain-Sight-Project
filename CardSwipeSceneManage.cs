using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSwipeSceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CardSwipeScene;
    void Start()
    {
        SceneHandle.cardScene = CardSwipeScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
