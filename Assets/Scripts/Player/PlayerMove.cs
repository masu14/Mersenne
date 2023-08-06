using System;
using UnityEngine;
using UniRx;

namespace Merusenne.Player
{
    public sealed class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D _rbody;
        private PlayerCore _playerCore;

        [SerializeField] private float speed = 3.0f;        //����
        [SerializeField] private float _jump = 9.0f;        //�W�����v��
        [SerializeField] private LayerMask _groundLayer;    //�n�ʃ��C���[�w��

        private bool _goJump = false;                       //�W�����v�t���O
        private bool _onGround = false;                     //�ڒn�t���O
        

        private ReactiveProperty<float> _axisH = new ReactiveProperty<float>();         //�̂̌���
        private ReactiveProperty<bool> _playerxDir = new ReactiveProperty<bool>(true);  //�̂̌���,�V���b�gPrefab�ɑ��M
        private ReactiveProperty<bool> _isGrounded = new BoolReactiveProperty();        //�ڒn����,

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
            
            _axisH.Value = Input.GetAxisRaw("Horizontal");         //���������̓��͂��`�F�b�N����

            //�����̒���
            if (_axisH.Value > 0.0f)
            {
                _playerxDir.Value = true;
            }
            else if (_axisH.Value < 0.0f)
            {
                _playerxDir.Value = false;
            }

            //�W�����v
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            //�n�㔻��
            ChecKGround();

            //���x�̍X�V
            if(IsGrounded.Value || _axisH.Value != 0)
            {
                _rbody.velocity = new Vector2(speed * _axisH.Value, _rbody.velocity.y);
            }

            //�W�����v����
            if(IsGrounded.Value && _goJump)    //�n�ʂ̏�and�W�����v�L�[
            {
                Debug.Log("�W�����v");
                Vector2 jumpPw = new Vector2(0, _jump);
                _rbody.AddForce(jumpPw, ForceMode2D.Impulse);   //������ɏu�ԓI�ȗ�

                _goJump = false;                                //�W�����v�t���O�����낷
            }
            
        }

        void Jump()
        {
            _goJump = true;
            Debug.Log("�W�����v�{�^������");
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
