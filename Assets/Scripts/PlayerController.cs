using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;                    //入力
    public bool playerxDirection = true;   //右向きかどうかの判定
    public bool playerRight = true;        //解除コード生成の向き

    private bool goShot = true;                         //解除コードフラグ
    [SerializeField] private float goShotTime = 1.5f;   //解除コード待機時間

    

    [SerializeField] private float speed = 3.0f;      //移動速度
    [SerializeField] private float jump = 9.0f;       //ジャンプ力
    [SerializeField] private LayerMask groundLayer;   //着地できるレイヤー

    bool goJump = false;            //ジャンプ開始フラグ
    bool onGround = false;          //接地フラグ

    [SerializeField] private ShotController shotBluePrefab;     //shotPrefabを指定
    [SerializeField] private ShotController shotGreenPrefab;
    [SerializeField] private ShotController shotRedPrefab;
    Vector3 shotPoint;                                      //解除コードの位置

    private int shotSwitch = 0;

    

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();                        //PlayerのRigidbody2D取得

        shotPoint = transform.Find("ShotPoint").localPosition;      //プレイヤーの子オブジェクトの位置取得
    }

    
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");     //水平方向の入力をチェックする

        //向きの調整
        if(axisH > 0.0f)        //右移動
        {
            transform.localScale = new Vector2(1, 1);
            playerxDirection = true;
        }
        else if(axisH < 0.0f)   //左移動
        {
            transform.localScale = new Vector2(-1, 1);  //左右反転させる
            playerxDirection = false;
        }

        //ジャンプ
        if(Input.GetButtonDown("Jump"))
        {
            Jump(); //ジャンプメソッドを呼び出す
        }

        //解除コード切り替え　blue=0, green=1, red=2
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shotSwitch++;
            if (shotSwitch > 2)
            {
                shotSwitch = 0;
            }
        }

        //解除コード発射
        if(Input.GetButtonDown("Fire1")　&& goShot == true)
        {
            Shot();                                     //ショットメソッドを呼び出す
            goShot = false;                             //解除コードフラグを下ろす
            Invoke(nameof(GoShot), goShotTime);         //解除コード待機時間経過後、GoShotメソッドを呼び出す
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

    public void Shot()      //解除コード生成
    {
        if(shotSwitch == 0) //青解除コードプレハブ生成
        {
            if (playerxDirection)
            {
                Instantiate(this.shotBluePrefab, transform.position + shotPoint, Quaternion.identity);    //右向き
                playerRight = true;
            }
            else
            {
                Instantiate(this.shotBluePrefab, transform.position + new Vector3(-shotPoint.x, shotPoint.y, 0), Quaternion.identity);    //左向き
                playerRight = false;
            }
        }

        if(shotSwitch == 1) //緑解除コードプレハブ生成
        {
            if (playerxDirection)
            {
                Instantiate(this.shotGreenPrefab, transform.position + shotPoint, Quaternion.identity);    //右向き
                playerRight = true;
            }
            else
            {
                Instantiate(this.shotGreenPrefab, transform.position + new Vector3(-shotPoint.x, shotPoint.y, 0), Quaternion.identity);    //左向き
                playerRight = false;
            }
        }

        if(shotSwitch == 2) //赤解除コードプレハブ生成
        {
            if (playerxDirection)
            {
                Instantiate(this.shotRedPrefab, transform.position + shotPoint, Quaternion.identity);    //右向き
                playerRight = true;
            }
            else
            {
                Instantiate(this.shotRedPrefab, transform.position + new Vector3(-shotPoint.x, shotPoint.y, 0), Quaternion.identity);    //左向き
                playerRight = false;
            }
        }
        
    }

    public void GoShot()
    {
        goShot = true;      //解除コードフラグを立てる
    }
}
