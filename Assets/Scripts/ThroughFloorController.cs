using UnityEngine;

public class ThroughFloorController : MonoBehaviour
{
    PlatformEffector2D platformEffector2D;
    
    void Start()
    {
        platformEffector2D = GetComponent<PlatformEffector2D>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            platformEffector2D.rotationalOffset = 180f;
        }
        else if (Input.GetButtonDown("Jump"))//InputEventProvider�ŏ������ׂ��Ȃ̂Ō�قǏC��
        {
            platformEffector2D.rotationalOffset = 0f;
        }
    }
}
