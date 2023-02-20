using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;     //入力
    public float speed = 3.0f;      //移動速度

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();        //PlayerのRigidbody2D取得
    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");     //水平方向の入力をチェックする

        //向きの調整
        if(axisH > 0.0f)        //右移動
        {
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1);
        }
        else if(axisH < 0.0f)   //左移動
        {
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1);  //左右反転させる
        }
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2(speed*axisH, rbody.velocity.y);   //速度を更新する   
    }
}
