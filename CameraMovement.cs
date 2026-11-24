using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Rigidbody2D player;
    public Rigidbody2D cam;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector3(player.position.x, player.position.y, -10);
        cam.position = position;
    }
}
