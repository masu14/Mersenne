using Merusenne.Player.InputImpls;
using UnityEngine;
using System;
using UniRx;

namespace Merusenne.StageGimmick
{
    /// <summary>
    /// 
    /// ThroughFroor(���蔲����)�𐧌䂷��N���X
    /// ���蔲�������v���C���[��y���W(����)���������ʒu�ɂ���Ƃ��R���C�_�[�������ɂȂ�
    /// �v���C���[�����蔲�����̏�ɏ���Ă���Ƃ��́A���������͂𒷉������邱�ƂŃR���C�_�[�������ɂȂ�
    /// 
    /// </summary>
    /// 
    public class ThroughFloorController : MonoBehaviour
    {
        private BoxCollider2D _myCollider;                      //�R���C�_�[��؂�ւ��邱�Ƃł��蔲�����s��
        private GameObject _player;                             //�v���C���[��y���W���R���C�_�[�̐؂�ւ��̏����Ɏg�p����
        private InputEventProviderImpl _inputEventProvider;     //�v���C���[�̉��������͂��R���C�_�[�̐؂�ւ��̏����Ɏg�p����


        private IDisposable _isDown;                            //���������͂̍w��
        private bool _canThroughDown = false;                   //���������͒������ł��蔲���t���O
        [SerializeField] private float _through_border = 0.2f;   //��������臒l 
        void Start()
        {
            _myCollider = GetComponent<BoxCollider2D>();                            //���蔲�����̃R���C�_�[�擾
            _player = GameObject.FindWithTag("Player");                             //�v���C���[�擾
            _inputEventProvider = _player.GetComponent<InputEventProviderImpl>();   //�v���C���[�̓��͎擾



            //���������͂𒷉����ōw�ǂ���
            _isDown = _inputEventProvider.IsThrough
                .Throttle(TimeSpan.FromSeconds(_through_border))
                .Subscribe(x => _canThroughDown = x)
                .AddTo(this);

        }

        void Update()
        {
            //���蔲���t���O������ �܂��� �v���C���[���㑤�ɂ���Ƃ��R���C�_�[�𖳌�������
            //2�ڂ̏����̓v���C���[�����ɂ߂荞�ނ̂�����邽��
            if (_canThroughDown || (_player.transform.position.y < transform.position.y))
            {
                _myCollider.enabled = false;    //������
            }
            else
            {
                _myCollider.enabled = true;     //�L����
            }
        }

    }

}
