using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform cam;
    public Transform player;
    void Start()
    {
        
    }

 
    void Update()
    {

        cam.position = new Vector3(player.position.x,player.position.y,cam.position.z);
    }
}
