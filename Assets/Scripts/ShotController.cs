using Merusenne.Player;
using UnityEngine;
using UniRx;

/// <summary>
/// �V���b�g�{�̂̐�����s���N���X
/// ShotxDir�v���p�e�B�̓V���b�g���ˌ�Ɉړ�������ω������Ȃ����߂ɕK�v
/// </summary>
public class ShotController : MonoBehaviour
{
    private PlayerMove _playerMove;
    
    //�p�����[�^
    [SerializeField] private float _shot_speed = 0.02f;   //�����R�[�h�̑���
    [SerializeField] private float _shot_time = 1.0f;     //�����R�[�h�̏��ł܂ł̎���


    private bool _isWrite = true;                       //��x�������������\�ȃt���O
    private bool _shotxDir;                             //�V���b�g�̔��ˌ���

    //�V���b�g���ˎ��̃v���C���[�̑̂̌������擾����
    private bool ShotxDir
    {
        get { return _shotxDir; }
        set
        {
            if(_isWrite)
            {
                _shotxDir = value;
                _isWrite = false;   //��x������������ȍ~�͏��������s��
            }
        }
    }

    private void Awake()
    {
        _playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();       //�v���C���[�̓����擾

        //�v���C���[�̑̂̌�������x�����擾�AOnDestroy����Dispose()�����悤�ɓo�^
        _playerMove.Observable.Subscribe(xDir => ShotxDir = xDir).AddTo(this);
    }

    private void FixedUpdate()
    {
        
        if (_shotxDir == true)                //�X�v���C�g���E�����̂Ƃ�
        {
            ShotMove(_shot_speed);
        }
        else                                  //�X�v���C�g���������̂Ƃ�
        {
            ShotMove(-_shot_speed);
        }

        //���������莞�Ԍo�ߌ�V���b�g����
        Destroy(gameObject, _shot_time);        
    }

    //�V���b�g�̑��x�̍X�V
    private void ShotMove(float shotSpeed)                             
    {
        transform.Translate(shotSpeed, 0, 0);
    }

    //�Փ˂���Ə��ł���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
