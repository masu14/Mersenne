using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    [SerializeField] private float shotSpeed = 0.02f;   //解除コードの速さ
    [SerializeField] private float shotTime = 1.0f;     //解除コードの消滅までの時間

    

    private void Start()
    {
        player = GameObject.FindWithTag("Player");                      //プレイヤーオブジェクト取得
        playerController = player.GetComponent<PlayerController>();     //プレイヤーコントローラー取得
    }
    private void Update()
    {
        if(playerController.playerRight == true)                        //スプライトが右向きのとき
        {
            ShotMove(shotSpeed);
            
        }
        else                                                            //スプライトが左向きのとき
        {
            ShotMove(-shotSpeed);
            
        }
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, shotTime);                                      //一定時間経過後解除コード消滅
    }

    private void ShotMove(float shotSpeed)                             //解除コードの移動
    {
        transform.Translate(shotSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("接触");
        Destroy(gameObject);
    }
}
