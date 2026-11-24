using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSceneManage : MonoBehaviour
{
    public GameObject wireScene;
    // Start is called before the first frame update
    void Start()
    {
        SceneHandle.wireScene = wireScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
