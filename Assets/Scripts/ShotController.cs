using Merusenne.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShotController : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private float shotSpeed = 0.02f;   //解除コードの速さ
    [SerializeField] private float shotTime = 1.0f;     //解除コードの消滅までの時間
    private PlayerMove _playerMove;

    private bool _shotxDir;



    private void Start()
    {
        player = GameObject.FindWithTag("Player");                      //プレイヤーオブジェクト取得
        _playerMove = player.GetComponent<PlayerMove>();     //プレイヤーコントローラー取得
    }
    private void Update()
    {
        _playerMove.Observable.Subscribe(xDir => _shotxDir = xDir);
        if (_shotxDir == true)                        //スプライトが右向きのとき
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
