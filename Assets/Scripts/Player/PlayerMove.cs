using System;
using UnityEngine;
using UniRx;

namespace Merusenne.Player
{
    public sealed class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D _rbody;                         
        float _axisH = 0.0f;                                //体の向き
        //bool _playerxDir = true;                            

        [SerializeField] private float speed = 3.0f;        //速さ
        [SerializeField] private float _jump = 9.0f;        //ジャンプ力
        [SerializeField] private LayerMask _groundLayer;    //地面レイヤー指定

        private bool _goJump = false;                       //ジャンプフラグ
        private bool _onGround = false;                     //接地フラグ

        private ReactiveProperty<bool> _playerxDir = new ReactiveProperty<bool>(true);
        public IObservable<bool> Observable
        {
            get { return _playerxDir; }
        }
        

        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
            
        }
        
        void Update()
        {
            
            _axisH = Input.GetAxisRaw("Horizontal");         //水平方向の入力をチェックする

            //向きの調整
            if(_axisH > 0.0f)
            {
                transform.localScale = new Vector2(1, 1);
                _playerxDir.Value = true;
            }
            else if(_axisH < 0.0f)
            {
                transform.localScale = new Vector2(-1, 1);
                _playerxDir.Value = false;
            }

            //ジャンプ
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            //地上判定
            _onGround = Physics2D.Linecast(transform.position, transform.position - transform.up * 0.1f, _groundLayer);

            //速度の更新
            if(_onGround || _axisH != 0)
            {
                _rbody.velocity = new Vector2(speed * _axisH, _rbody.velocity.y);
            }

            //ジャンプ処理
            if(_onGround && _goJump)    //地面の上andジャンプキー
            {
                Debug.Log("ジャンプ");
                Vector2 jumpPw = new Vector2(0, _jump);
                _rbody.AddForce(jumpPw, ForceMode2D.Impulse);   //上向きに瞬間的な力

                _goJump = false;                                //ジャンプフラグを下ろす
            }
        }

        void Jump()
        {
            _goJump = true;
            Debug.Log("ジャンプボタン押し");
        }
    }

}
