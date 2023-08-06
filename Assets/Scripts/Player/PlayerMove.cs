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

        //�A�j���[�V����
        private Animator animator;
        private string _stopAnime = "PlayerStop";
        private string _moveAnime = "PlayerMove";
        private string _jumpAnime = "PlayerJump";
        private string _deadAnime = "PlayerOver";
        private string _nowAnime = "";
        private string _oldAnime = "";

        private ReactiveProperty<bool> _playerxDir = new ReactiveProperty<bool>(true);
        
        public IObservable<bool> Observable
        {
            get { return _playerxDir; }
        }
        

        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            

        }

        private void Start()
        {
            _nowAnime = _stopAnime;
            _oldAnime = _stopAnime;
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

            if (_onGround)
            {
                if (_axisH == 0)
                {
                    _nowAnime = _stopAnime;
                }
                else
                {
                    _nowAnime = _moveAnime;
                }
            }
            else
            {
                //��
                _nowAnime = _jumpAnime;
            }

            if(_nowAnime != _oldAnime)
            {
                _oldAnime = _nowAnime;
                animator.Play(_nowAnime);
            }
        }

        void Jump()
        {
            _goJump = true;
            Debug.Log("�W�����v�{�^������");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Dead")
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            animator.Play(_deadAnime);
        }
    }

}
