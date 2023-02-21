using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;     //入力
    public float speed = 3.0f;      //移動速度
    public float jump = 9.0f;       //ジャンプ力
    public LayerMask groundLayer;   //着地できるレイヤー

    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //接地フラグ

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();        //PlayerのRigidbody2D取得
    }

    
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

        //ジャンプ
        if(Input.GetButtonDown("Jump"))
        {
            Jump(); //ジャンプメソッドを呼び出す
        }
    }

    void FixedUpdate()
    {
        //地上判定
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(onGround || axisH != 0)  //地面の上 or 速度が0でない
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);   //速度を更新する
        }

        if(onGround && goJump)      //地面の上でジャンプキーが押された
        {
            Debug.Log("ジャンプ");
            Vector2 jumpPw = new Vector2(0, jump);                          //上向きのベクトル
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);                    //瞬間的な力を加える

            goJump = false;                                                 //ジャンプフラグを下ろす
        }

           

    }

    public void Jump()
    {
        goJump = true;      //ジャンプフラグを立てる
        Debug.Log("ジャンプボタン押し");
    }
}
