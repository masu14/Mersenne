using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if(playerController.playerRight == true)
        {
            transform.Translate(0.02f, 0, 0);
            Debug.Log("右向きショット");
        }
        else
        {
            transform.Translate(-0.02f, 0, 0);
            Debug.Log("左向きショット");
        }
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("接触");
        Destroy(gameObject);
    }
}
