using System;
using UnityEngine;
using UniRx;

namespace Merusenne.Player
{
    public sealed class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D _rbody;
        private PlayerCore _playerCore;

        [SerializeField] private float speed = 3.0f;        //速さ
        [SerializeField] private float _jump = 9.0f;        //ジャンプ力
        [SerializeField] private LayerMask _groundLayer;    //地面レイヤー指定

        private bool _goJump = false;                       //ジャンプフラグ
        private bool _onGround = false;                     //接地フラグ
        

        private ReactiveProperty<float> _axisH = new ReactiveProperty<float>();         //体の向き
        private ReactiveProperty<bool> _playerxDir = new ReactiveProperty<bool>(true);  //体の向き,ショットPrefabに送信
        private ReactiveProperty<bool> _isGrounded = new BoolReactiveProperty();        //接地判定,

        public IReactiveProperty<float> OnAxisH => _axisH;
        public IObservable<bool> Observable
        {
            get { return _playerxDir; }
        }
        public IReactiveProperty<bool> IsGrounded => _isGrounded;

        private void Awake()
        {
            _axisH.AddTo(this);
            _isGrounded.AddTo(this);

            _rbody = GetComponent<Rigidbody2D>();
            _playerCore = GetComponent<PlayerCore>();

            _playerCore.OnDead.Subscribe(_ => DeadMove()).AddTo(this);
        }

        void Update()
        {
            
            _axisH.Value = Input.GetAxisRaw("Horizontal");         //水平方向の入力をチェックする

            //向きの調整
            if (_axisH.Value > 0.0f)
            {
                _playerxDir.Value = true;
            }
            else if (_axisH.Value < 0.0f)
            {
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
            ChecKGround();

            //速度の更新
            if(IsGrounded.Value || _axisH.Value != 0)
            {
                _rbody.velocity = new Vector2(speed * _axisH.Value, _rbody.velocity.y);
            }

            //ジャンプ処理
            if(IsGrounded.Value && _goJump)    //地面の上andジャンプキー
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

        private void ChecKGround()
        {
            _onGround = Physics2D.Linecast(transform.position, transform.position - transform.up * 0.1f, _groundLayer);

            IsGrounded.Value = _onGround;
        }

        private void DeadMove()
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            _rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
    }

}
