using System;
using UnityEngine;
using UniRx;


namespace Merusenne.Player
{
    public sealed class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D _rbody;
        private PlayerCore _playerCore;
        private IInputEventProvider _inputEventProvider;

        [SerializeField] private float _xDashSpeed = 3.0f;  //地上での速さ
        //[SerializeField] private float _xFriSpeed = 3.0f; 　　//空中での速さ
        [SerializeField] private float _jump = 9.0f;        //ジャンプ力
        [SerializeField] private float _deadJump = 10f;     //dead時演出のジャンプ
        [SerializeField] private float _deadGravity = 30f;  //dead時演出の落下
        [SerializeField] private LayerMask _groundLayer;    //地面レイヤー指定
        [SerializeField] private AnimationCurve _speedCurve;//横移動の速度のグラフ
        [SerializeField] private AnimationCurve _jumpCurve; //ジャンプの速度のグラフ

        private bool _isJump = false;                       //ジャンプフラグ
        private bool _onGround = false;                     //接地フラグ
        private bool _isDead = false;                       //Deadフラグ
        private float _dashTime;                            //_speedCurveの定義域
        private float _jumpTime;                            //_jumpCurveの定義域
        private float beforeAxisH;                          //前フレームの横入力
        //private float _groundDistance = 0.5f;
        private Vector3 _playerWide = new Vector3(0.35f, 0, 0);


        
        private ReactiveProperty<bool> _playerxDir = new ReactiveProperty<bool>(true);  //体の向き,ショットPrefabに送信
        private ReactiveProperty<bool> _isGrounded = new BoolReactiveProperty();        //接地判定,

        public IObservable<bool> Observable
        {
            get { return _playerxDir; }
        }
        public IReactiveProperty<bool> IsGrounded => _isGrounded;

        Vector2 jumpPw;
        private void Awake()
        {
            
            _isGrounded.AddTo(this);

            _rbody = GetComponent<Rigidbody2D>();
            _playerCore = GetComponent<PlayerCore>();
            _inputEventProvider = GetComponent<IInputEventProvider>();

            _playerCore.OnDead.Subscribe(_ => DeadMove()).AddTo(this);
        }

        private void Start()
        {
            jumpPw = new Vector2(0, _jump * _jumpCurve.Evaluate(_jumpTime));
        }
        void Update()
        {
            //向きの調整
            if (_inputEventProvider.AxisH.Value > 0.15f)
            {
                _playerxDir.Value = true;
            }
            else if (_inputEventProvider.AxisH.Value < -0.15f)
            {
                _playerxDir.Value = false;
            }

            if (_inputEventProvider.IsJump.Value && _isGrounded.Value)
            {
                _isJump = true;
            }
        }

        private void FixedUpdate()
        {
            //地上判定
            ChecKGround();

            //速度の更新
            if (_inputEventProvider.AxisH.Value != 0) //横入力有
            {
                _dashTime += Time.deltaTime;
                _rbody.velocity = new Vector2(_xDashSpeed * _inputEventProvider.AxisH.Value * _speedCurve.Evaluate(_dashTime), _rbody.velocity.y);
            }
            else if(_inputEventProvider.AxisH.Value ==0)　//横入力無
            {
                _dashTime = 0.0f;
                _rbody.velocity = new Vector2(0, _rbody.velocity.y);
            }
           

            //横入力切替時に横移動の速度曲線を初期化
            if(_inputEventProvider.AxisH.Value > 0 && beforeAxisH < 0)
            {
                _dashTime = 0.0f;
            }
            else if(_inputEventProvider.AxisH.Value <0&&beforeAxisH > 0)
            {
                _dashTime = 0.0f;
            }
            beforeAxisH = _inputEventProvider.AxisH.Value;
            


            //ジャンプ処理
            if (IsGrounded.Value && _isJump)    //地面の上andジャンプキー
            {
                _isJump = false;
                Debug.Log("ジャンプ");
                
                _rbody.velocity = jumpPw;
                //_rbody.AddForce(jumpPw, ForceMode2D.Impulse);   //上向きに瞬間的な力
                
                
            }
           
            //Dead時のジャンプ
            if (_isDead)
            {
                transform.localPosition += new Vector3(0, _deadJump) * Time.deltaTime;
                _deadJump -= _deadGravity * Time.deltaTime;
            }
            
        }

        

        //地上判定
        private bool ChecKGround()
        {
            bool isGround = Physics2D.Linecast(transform.position + _playerWide, transform.position + _playerWide - transform.up * 0.1f, _groundLayer) ||
                            Physics2D.Linecast(transform.position, transform.position - transform.up * 0.1f, _groundLayer) ||
                            Physics2D.Linecast(transform.position - _playerWide, transform.position - _playerWide - transform.up * 0.1f, _groundLayer);

            _onGround = isGround;
            IsGrounded.Value = _onGround;

            return _onGround;
        }

        //Dead時に跳ね上がる動き
        private void DeadMove()
        {
            GetComponent<EdgeCollider2D>().enabled = false;
            _rbody.velocity = Vector2.zero;
            _isDead = true;
        }
    }

}
