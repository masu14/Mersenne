using System;
using UnityEngine;
using UniRx;


namespace Merusenne.Player
{
    /// <summary>
    /// �v���C���[�̓����𐧌䂷��N���X
    /// </summary>
    public sealed class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D _rbody;
        private PlayerCore _playerCore;
        private IInputEventProvider _inputEventProvider;

        //�v���C���[�̈ړ��̃p�����[�^
        [SerializeField] private float _xDashSpeed = 3.0f;                  //�n��ł̑���
        [SerializeField] private float _xFriSpeed = 2.5f; �@�@              //�󒆂ł̑���
        [SerializeField] private Vector2 jumpSpeed = new Vector2(0, 14);    //�W�����v��
        [SerializeField] private float _deadJump = 10f;                     //dead�����o�̃W�����v
        [SerializeField] private LayerMask _groundLayer;                    //�n�ʃ��C���[�w��
        [SerializeField] private AnimationCurve _speedCurve;                //���ړ��̑��x�̃O���t
        [SerializeField] private AnimationCurve _jumpCurve;                 //�W�����v�̑��x�̃O���t

        //�t���O
        private bool _isJump = false;                           //�W�����v�t���O
        private bool _onGround = false;                         //�ڒn�t���O
        
        
        private float _dashTime;                                //_speedCurve�̒�`��
        private float beforeAxisH;                              //�O�t���[���̉�����
        private Vector3 _playerWide = new Vector3(0.35f, 0, 0); //�v���C���[�̉���
        private const float _AXISHBORDER = 0.15f;               //�����͂�臒l


        private ReactiveProperty<bool> _playerxDir = new ReactiveProperty<bool>(true);  //�̂̌���,�V���b�gPrefab�ɑ��M
        private ReactiveProperty<bool> _isGrounded = new BoolReactiveProperty();        //�ڒn����,

        //�̂̌����A�ڒn����̑��M
        public IObservable<bool> Observable
        {
            get { return _playerxDir; }
        }
        public IReactiveProperty<bool> IsGrounded => _isGrounded;

        
        private void Awake()
        {
            
            _rbody = GetComponent<Rigidbody2D>();                       //�v���C���[�̓����擾
            _playerCore = GetComponent<PlayerCore>();                   //�v���C���[�̏�Ԏ擾(Dead�t���O)
            _inputEventProvider = GetComponent<IInputEventProvider>();  //�v���C���[�̓��͎擾

            //OnDestroy����Dispose()�����悤�ɓo�^
            _isGrounded.AddTo(this);
            _playerCore.OnDead.Subscribe(_ => DeadMove()).AddTo(this);
        }

        //���͂̑��M�̌��m��Update�ŏ�������
        void Update()
        {
            //Dead�t���O���������珈�����s��Ȃ�
            if (_playerCore.IsDead.Value) return;

            //�����̒���
            if (_inputEventProvider.AxisH.Value > _AXISHBORDER)
            {
                _playerxDir.Value = true;
            }
            else if (_inputEventProvider.AxisH.Value < -_AXISHBORDER)
            {
                _playerxDir.Value = false;
            }

            //�n��ŃW�����v���͂���������W�����v�t���O�𗧂Ă�
            if (_inputEventProvider.IsJump.Value && _isGrounded.Value)
            {
                _isJump = true;
            }
        }

        private void FixedUpdate()
        {
            //Dead�t���O���������珈�����s��Ȃ�
            if (_playerCore.IsDead.Value) return;

            //�n�㔻��
            ChecKGround();

            //���x�̍X�V
            if (_inputEventProvider.AxisH.Value != 0 && _onGround) //�����͗L���n��
            {
                _dashTime += Time.deltaTime;
                _rbody.velocity = new Vector2(_xDashSpeed * _inputEventProvider.AxisH.Value * _speedCurve.Evaluate(_dashTime), _rbody.velocity.y);
            }
            else if(_inputEventProvider.AxisH.Value != 0 && _onGround == false) //�����͗L����
            {
                _dashTime += Time.deltaTime;
                _rbody.velocity = new Vector2(_xFriSpeed * _inputEventProvider.AxisH.Value * _speedCurve.Evaluate(_dashTime), _rbody.velocity.y);
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
            if (IsGrounded.Value && _isJump)    //�n�ʂ̏さ�W�����v�t���O
            {
                _isJump = false;
                Debug.Log("�W�����v");
                _rbody.velocity = jumpSpeed;
            }

           
        }

        

        //�n�㔻��
        private bool ChecKGround()
        {
            //�����̒��S�A�E�[�A���[�̂����ꂩ���n�ʂɐG��Ă���Ȃ�t���O�𗧂Ă�
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
            _rbody.velocity = new Vector2(0, _deadJump);
        }
    }

}
