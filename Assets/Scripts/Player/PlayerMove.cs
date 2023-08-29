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

        [SerializeField] private float _xDashSpeed = 3.0f;  //�n��ł̑���
        //[SerializeField] private float _xFriSpeed = 3.0f; �@�@//�󒆂ł̑���
        [SerializeField] private float _jump = 9.0f;        //�W�����v��
        [SerializeField] private float _deadJump = 10f;     //dead�����o�̃W�����v
        [SerializeField] private float _deadGravity = 30f;  //dead�����o�̗���
        [SerializeField] private LayerMask _groundLayer;    //�n�ʃ��C���[�w��
        [SerializeField] private AnimationCurve _speedCurve;//���ړ��̑��x�̃O���t
        [SerializeField] private AnimationCurve _jumpCurve; //�W�����v�̑��x�̃O���t

        private bool _isJump = false;                       //�W�����v�t���O
        private bool _onGround = false;                     //�ڒn�t���O
        private bool _isDead = false;                       //Dead�t���O
        private float _dashTime;                            //_speedCurve�̒�`��
        private float _jumpTime;                            //_jumpCurve�̒�`��
        private float beforeAxisH;                          //�O�t���[���̉�����
        //private float _groundDistance = 0.5f;
        private Vector3 _playerWide = new Vector3(0.35f, 0, 0);


        
        private ReactiveProperty<bool> _playerxDir = new ReactiveProperty<bool>(true);  //�̂̌���,�V���b�gPrefab�ɑ��M
        private ReactiveProperty<bool> _isGrounded = new BoolReactiveProperty();        //�ڒn����,

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
            //�����̒���
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
            //�n�㔻��
            ChecKGround();

            //���x�̍X�V
            if (_inputEventProvider.AxisH.Value != 0) //�����͗L
            {
                _dashTime += Time.deltaTime;
                _rbody.velocity = new Vector2(_xDashSpeed * _inputEventProvider.AxisH.Value * _speedCurve.Evaluate(_dashTime), _rbody.velocity.y);
            }
            else if(_inputEventProvider.AxisH.Value ==0)�@//�����͖�
            {
                _dashTime = 0.0f;
                _rbody.velocity = new Vector2(0, _rbody.velocity.y);
            }
           

            //�����͐ؑ֎��ɉ��ړ��̑��x�Ȑ���������
            if(_inputEventProvider.AxisH.Value > 0 && beforeAxisH < 0)
            {
                _dashTime = 0.0f;
            }
            else if(_inputEventProvider.AxisH.Value <0&&beforeAxisH > 0)
            {
                _dashTime = 0.0f;
            }
            beforeAxisH = _inputEventProvider.AxisH.Value;
            


            //�W�����v����
            if (IsGrounded.Value && _isJump)    //�n�ʂ̏�and�W�����v�L�[
            {
                _isJump = false;
                Debug.Log("�W�����v");
                
                _rbody.velocity = jumpPw;
                //_rbody.AddForce(jumpPw, ForceMode2D.Impulse);   //������ɏu�ԓI�ȗ�
                
                
            }
           
            //Dead���̃W�����v
            if (_isDead)
            {
                transform.localPosition += new Vector3(0, _deadJump) * Time.deltaTime;
                _deadJump -= _deadGravity * Time.deltaTime;
            }
            
        }

        

        //�n�㔻��
        private bool ChecKGround()
        {
            bool isGround = Physics2D.Linecast(transform.position + _playerWide, transform.position + _playerWide - transform.up * 0.1f, _groundLayer) ||
                            Physics2D.Linecast(transform.position, transform.position - transform.up * 0.1f, _groundLayer) ||
                            Physics2D.Linecast(transform.position - _playerWide, transform.position - _playerWide - transform.up * 0.1f, _groundLayer);

            _onGround = isGround;
            IsGrounded.Value = _onGround;

            return _onGround;
        }

        //Dead���ɒ��ˏオ�铮��
        private void DeadMove()
        {
            GetComponent<EdgeCollider2D>().enabled = false;
            _rbody.velocity = Vector2.zero;
            _isDead = true;
        }
    }

}
