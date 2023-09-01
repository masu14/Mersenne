using Merusenne.Player.InputImpls;
using UnityEngine;
using System;
using UniRx;


/// <summary>
/// ThroughFroor(���蔲����)�𐧌䂷��N���X
/// </summary>
public class ThroughFloorController : MonoBehaviour
{
    private BoxCollider2D _myCollider;
    private GameObject _player;
    private InputEventProviderImpl _inputEventProvider;
    

    private IDisposable _isDown;                            //���������͂̍w��
    private bool _canThroughDown = false;                   //���������͒������ł��蔲���t���O
    [SerializeField] private float _throughBorder = 0.2f;   //��������臒l 
    void Start()
    {
        _myCollider = GetComponent<BoxCollider2D>();                            //���蔲�����̃R���C�_�[�擾
        _player = GameObject.FindWithTag("Player");                             //�v���C���[�擾
        _inputEventProvider = _player.GetComponent<InputEventProviderImpl>();   //�v���C���[�̓��͎擾

        

        //���������͂𒷉����ōw�ǂ���
        _isDown = _inputEventProvider.IsThrough
            .Throttle(TimeSpan.FromSeconds(_throughBorder))
            .Subscribe(x => _canThroughDown = x)
            .AddTo(this);

    }

    void Update()
    {
        //���蔲���t���O������ �܂��� �v���C���[���㑤�ɂ���Ƃ��R���C�_�[�𖳌�������
        //2�ڂ̏����̓v���C���[�����ɂ߂荞�ނ̂�����邽��
        if(_canThroughDown ||(_player.transform.position.y < transform.position.y))
        {
            _myCollider.enabled = false;
        }
        else
        {
            _myCollider.enabled = true;
        }
    }

}
