using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughFloorController : MonoBehaviour
{
    PlatformEffector2D platformEffector2D;
    // Start is called before the first frame update
    void Start()
    {
        platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            platformEffector2D.rotationalOffset = 180f;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            platformEffector2D.rotationalOffset = 0f;
        }
    }
}
