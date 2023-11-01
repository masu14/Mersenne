using System;
using UniRx;
using UnityEngine;

namespace Merusenne.Player
{
    /// <summary>
    /// �v���C���[�L�����̃A�j���[�V�����������s���N���X
    /// �A�j���[�^�[�̎��s�^�C�~���O���`����
    /// ���͂�v���C���[�̏�Ԃɉ����ăA�j���[�V������؂�ւ���
    /// </summary>
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;                             //�v���C���[��Animator�𐧌䂷��
        private PlayerMove _playerMove;                         //�v���C���[�̓����𐧌䂷��
        private PlayerCore _playerCore;                         //�v���C���[��Dead��ԂɂȂ����Ƃ��Ɏg�p����
        private IInputEventProvider _inputEventProvider;        //���͂��擾����A���͂ɉ����ă��[�V������؂�ւ���

        private bool _isClear = false;

        //�e��A�j���[�V�����̕R�Â�
        private string _stopAnime = "PlayerStop";   //�Î~���[�V����(�����͎�)
        private string _moveAnime = "PlayerMove";   //�n��ړ����[�V����(�n��ŉ�����)
        private string _jumpAnime = "PlayerJump";   //�󒆃��[�V����(�󒆎�)
        private string _deadAnime = "PlayerOver";   //Dead���[�V����(Dead�^�O�ڐG��)

        private string _nowAnime = "";              //���݂̃t���[���̃��[�V����
        private string _oldAnime = "";              //1�t���[���O�̃��[�V����

        private const float _AXISHBORDER = 0.15f;    //�����͂�臒l

        private void Awake()
        {
            _animator = GetComponent<Animator>();                       //�v���C���[��Animator�擾
            _playerMove = GetComponent<PlayerMove>();                   //�v���C���[�̓����擾(�ڒn������w��)
            _playerCore = GetComponent<PlayerCore>();                   //�v���C���[�̏�Ԏ擾(Dead�ʒm���w��)
            _inputEventProvider = GetComponent<IInputEventProvider>();  //�v���C���[�̓��͎擾

            //Dead�ʒm���󂯎�����Ƃ���Dead���[�V�������s�����߂̍w��
            _playerCore.OnDead.Subscribe(_ => Dead()).AddTo(this);
        }

        private void Start()
        {
            //�A�j���[�V�����̏�����
            _nowAnime = _stopAnime;
            _oldAnime = _stopAnime;

            _isClear = false;
        }
        void Update()
        {
            if (_isClear) return;
            //�v���C���[��Dead���ɂ͏������s��Ȃ�
            if (_playerCore.IsDead.Value) return;
            
            //�����̒����A�����͂�臒l�𒴂�����E����(������)�ɂ���
            if (_inputEventProvider.AxisH.Value > _AXISHBORDER)         //�E����
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (_inputEventProvider.AxisH.Value < -_AXISHBORDER)   //������
            {
                transform.localScale = new Vector2(-1, 1);
            }

            //�v���C���[�̐ڒn���肩��n��Ƌ󒆂̃��[�V������؂�ւ���
            if (_playerMove.IsGrounded.Value)
            {
                //�n��
                if (Math.Abs(_inputEventProvider.AxisH.Value) <= _AXISHBORDER)
                {
                    //�Î~
                    _nowAnime = _stopAnime;
                }
                else
                {
                    //�ړ�
                    _nowAnime = _moveAnime;
                }
            }
            else
            {
                //��
                _nowAnime = _jumpAnime;
            }

            //�A�j���[�V�����̐؂�ւ����Ď�����
            if (_nowAnime != _oldAnime)
            {
                _oldAnime = _nowAnime;
                _animator.Play(_nowAnime);
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Goal")
            {
                _isClear = true;
                _animator.Play(_stopAnime);
            }
        }

        //�v���C���[��Dead�����Ƃ���Dead���[�V�������s��
        private void Dead()
        {
            _animator.Play(_deadAnime);
        }
    }

}
