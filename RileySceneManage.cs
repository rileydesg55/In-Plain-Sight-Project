using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RileySceneManage : MonoBehaviour
{

    public GameObject rileyScene;
    // Start is called before the first frame update
    void Start()
    {
        SceneHandle.rileyScene = rileyScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
