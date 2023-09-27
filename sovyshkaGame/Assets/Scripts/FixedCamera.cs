using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 camera = transform.position;
        camera.x = player.position.x;
        camera.y = player.position.y;   

        transform.position = camera;
    }
}
