using UnityEngine;
using UniRx;

/// <summary>
/// �J�����̈ʒu�𐧌䂷��N���X�A�X�e�[�W���Ƃ̒�_�J�����Ńv���C���[�̂���X�e�[�W���ʂ�
/// 
/// �d�l�F�J�����̓v���C���[���X�e�[�W�̃R���C�_�[�ɐڐG�����炻�̃X�e�[�W���ʂ�
///       ���[�h���ɍŌ�ɐG�ꂽ�Z�[�u�|�C���g�̂���X�e�[�W���ʂ�
///       
/// �o�O�F���[�h����GameManager�N���X���瑗�M�����Z�[�u�f�[�^(�Z�[�u�|�C���g�̂���X�e�[�W�̍��W)���w�ǂ��ꂸ�A
///     �@(x,y)=(cameraWidth,cameraHeight)�̃X�e�[�W���ʂ��Ă��܂��B
///     �@
/// ��ֈāFUpdate���\�b�h�Ŗ��t���[���A�v���C���[�ƃX�e�[�W�̐ڐG�̌��m���Ď����ăJ�����������s��
///         ���̕��@��Start���\�b�h���ɍs���Ȃ��̂ŁA��u����(x,y)=(cameraWidth,cameraHeight)�̃X�e�[�W���ʂ��Ă��܂��B
///         �܂�Update�ŌĂԂ͖̂��ʂ̕��ׂ������Ă��܂��B
/// </summary>
/// 
public class CameraManager : MonoBehaviour
{
    
    private const int _CAMERAWIDE = 18;                     //��ʉ���
    private const int _CAMERAHEIGHT = 10;                    //��ʏc��
    private float _edgeRight, _edgeLeft, _edgeUp, _edgeDown;    //�J�����ɉf��[�̍��W����                                   

    private Vector2 _nowStage;                              //�v���C���[������X�e�[�W

    void Awake()
    {
        var _gameManager = FindObjectOfType<GameManager>();

        //�o�O�Y���ӏ��A���[�h���ɑ��M�����Z�[�u�f�[�^�̍w�ǂ����鏈��
        /*
        _gameManager.OnSaveStage
           .Subscribe(x =>
           {
               UpdateCameraPos(x);
               Debug.Log($"���[�h���̃X�e�[�W�̍��W{x}");

           })
           .AddTo(this);
        */

        StageManager[] stageManagers = FindObjectsOfType<StageManager>();

        foreach (StageManager stageManager in stageManagers)
        {
            stageManager.OnPlayerEnter.Subscribe(pos => {
               // UpdateCameraPos(pos);    //�{���̓v���C���[�ƃX�e�[�W�̐ڐG�����m������J���������̃��\�b�h���Ăяo���̂��]�܂���
                _nowStage = stageManager.transform.position;        //��ֈāA�ڐG�����X�e�[�W��_nowStage�Ɋi�[
            } )
                .AddTo(this);
        }


    }

    private void Start()
    {
        
    }

    //  �J���������̃��\�b�h
    private void UpdateCameraPos(Vector2 nowStagePos)
    {
        _edgeLeft = nowStagePos.x - _CAMERAWIDE / 2;
        _edgeRight = nowStagePos.x + _CAMERAWIDE / 2;
        _edgeUp = nowStagePos.y + _CAMERAHEIGHT / 2;
        _edgeDown = nowStagePos.y - _CAMERAHEIGHT / 2;

        if(_edgeLeft >= transform.position.x)
        {
            transform.position += _CAMERAWIDE * Vector3.right;
        }
        else if(_edgeRight <= transform.position.x)
        {
            transform.position -= _CAMERAWIDE * Vector3.right;
        }

        if(_edgeDown >= transform.position.y)
        {
            transform.position += _CAMERAHEIGHT * Vector3.up;
        }
        else if (_edgeUp <= transform.position.y)
        {
            transform.position -= _CAMERAHEIGHT * Vector3.up;
        }
    
    }
    

    //��ֈāA_nowStage����J�����̈ʒu���X�V����
    
    void Update()
    {
        _edgeLeft = _nowStage.x - _CAMERAWIDE / 2;
        _edgeRight = _nowStage.x + _CAMERAWIDE / 2;
        _edgeUp = _nowStage.y + _CAMERAHEIGHT / 2;
        _edgeDown = _nowStage.y - _CAMERAHEIGHT / 2;

        //�J�����X�V
        if (_edgeLeft >= transform.position.x)
        {
            transform.position += _CAMERAWIDE * Vector3.right;
        }
        else if (_edgeRight <= transform.position.x)
        {
            transform.position -= _CAMERAWIDE * Vector3.right;
        }

        if(_edgeDown >= transform.position.y)
        {
            transform.position += _CAMERAHEIGHT * Vector3.up;
        }
        else if (_edgeUp <= transform.position.y)
        {
            transform.position -= _CAMERAHEIGHT * Vector3.up;
        }
    }
    
    
    
}
