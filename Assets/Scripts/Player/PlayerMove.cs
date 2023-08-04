using System;
using UnityEngine;
using UniRx;

namespace Merusenne.Player
{
    public sealed class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D _rbody;                         
        float _axisH = 0.0f;                                //�̂̌���
        //bool _playerxDir = true;                            

        [SerializeField] private float speed = 3.0f;        //����
        [SerializeField] private float _jump = 9.0f;        //�W�����v��
        [SerializeField] private LayerMask _groundLayer;    //�n�ʃ��C���[�w��

        private bool _goJump = false;                       //�W�����v�t���O
        private bool _onGround = false;                     //�ڒn�t���O

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
            
            _axisH = Input.GetAxisRaw("Horizontal");         //���������̓��͂��`�F�b�N����

            //�����̒���
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

            //�W�����v
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            //�n�㔻��
            _onGround = Physics2D.Linecast(transform.position, transform.position - transform.up * 0.1f, _groundLayer);

            //���x�̍X�V
            if(_onGround || _axisH != 0)
            {
                _rbody.velocity = new Vector2(speed * _axisH, _rbody.velocity.y);
            }

            //�W�����v����
            if(_onGround && _goJump)    //�n�ʂ̏�and�W�����v�L�[
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
    }

}
