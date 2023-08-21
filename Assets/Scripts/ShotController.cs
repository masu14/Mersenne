using Merusenne.Player;
using UnityEngine;
using UniRx;

public class ShotController : MonoBehaviour
{
    private GameObject _player;
    private bool _isWrite = true;
    [SerializeField] private float shotSpeed = 0.02f;   //�����R�[�h�̑���
    [SerializeField] private float shotTime = 1.0f;     //�����R�[�h�̏��ł܂ł̎���
    private PlayerMove _playerMove;

    private bool _shotxDir;

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
        _player = GameObject.FindWithTag("Player");                      //�v���C���[�I�u�W�F�N�g�擾
        
        _playerMove = _player.GetComponent<PlayerMove>();                //�v���C���[�R���g���[���[�擾
        _playerMove.Observable.Subscribe(xDir => ShotxDir = xDir);
    }
   
    private void Update()
    {
        

        
    }

    private void FixedUpdate()
    {
        
        if (_shotxDir == true)                        //�X�v���C�g���E�����̂Ƃ�
        {
            ShotMove(shotSpeed);
        }
        else                                                            //�X�v���C�g���������̂Ƃ�
        {
            ShotMove(-shotSpeed);
            Debug.Log("�������V���b�g");
        }

        Destroy(gameObject, shotTime);                                      //��莞�Ԍo�ߌ�����R�[�h����
    }

    private void ShotMove(float shotSpeed)                             //�����R�[�h�̈ړ�
    {
        transform.Translate(shotSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�ڐG");
        Destroy(gameObject);
    }
}
