using UnityEngine;
using UniRx;

/// <summary>
/// 
/// �J�����̈ʒu�𐧌䂷��N���X�A�v���C���[�̂���X�e�[�W�̍��W��StageManager����w�ǂ��A�v���C���[�̂���X�e�[�W���ʂ��B
/// 
/// �d�l�F�J�����̓v���C���[���X�e�[�W�̃R���C�_�[�ɐڐG�����炻�̃X�e�[�W���ʂ�(StageManager����w��)
///       ���[�h���ɍŌ�ɐG�ꂽ�Z�[�u�|�C���g�̂���X�e�[�W���ʂ�(GameManager����w��)
///       
/// �o�O�F���[�h����GameManager�N���X���瑗�M�����Z�[�u�f�[�^(�Z�[�u�|�C���g�̂���X�e�[�W�̍��W)���w�ǂ��ꂸ�A
///     �@(x,y)=(cameraWidth,cameraHeight)�̃X�e�[�W���ʂ��Ă��܂��B
///     �@
/// ��ֈāFUpdate���\�b�h�Ŗ��t���[���A�v���C���[�ƃX�e�[�W�̐ڐG�̌��m���Ď����ăJ�����������s��
///         ���̕��@��Start���\�b�h���ɍs���Ȃ��̂ŁA��u����(x,y)=(cameraWidth,cameraHeight)�̃X�e�[�W���ʂ��Ă��܂��B
///         �܂�Update�ŌĂԂ͖̂��ʂ̕��ׂ������Ă��܂��B
///         
/// </summary>
/// 
public class CameraManager : MonoBehaviour
{
    
    private const int _CAMERAWIDE = 18;                         //��ʉ���
    private const int _CAMERAHEIGHT = 10;                       //��ʏc��
    private float _edgeRight, _edgeLeft, _edgeUp, _edgeDown;    //�J�����ɉf��[�̍��W����                                   
    private Vector2 _nowStage;                                  //�v���C���[������X�e�[�W

    void Awake()
    {
        //�o�O�Y���ӏ��A���[�h���ɑ��M�����Z�[�u�f�[�^�̍w�ǂ����鏈��
        /*
        var _gameManager = FindObjectOfType<GameManager>();

       
        _gameManager.OnSaveStage
           .Subscribe(x =>
           {
               UpdateCameraPos(x);
               Debug.Log($"���[�h���̃X�e�[�W�̍��W{x}");

           })
           .AddTo(this);
        */

        StageManager[] stageManagers = FindObjectsOfType<StageManager>();   //���[�h���ɃV�[����̂��ׂẴX�e�[�W�N���X���擾���Ă���

        //�e�X�e�[�W�N���X�̃X�e�[�W�̍��W�̍w��
        foreach (StageManager stageManager in stageManagers)
        {
            stageManager.OnPlayerEnter
                .Subscribe(pos => {
               // UpdateCameraPos(pos);    //�{���̓v���C���[�ƃX�e�[�W�̐ڐG�����m������J���������̃��\�b�h���Ăяo���̂��]�܂���
                _nowStage = stageManager.transform.position;        //��ֈāA�ڐG�����X�e�[�W��_nowStage�Ɋi�[
            } )
                .AddTo(this);
        }


    }


    //  �J���������̃��\�b�h�A�����̍��W����J�����̈ʒu���킹���s��
    private void UpdateCameraPos(Vector2 nowStagePos)
    {
        _edgeLeft = nowStagePos.x - _CAMERAWIDE / 2;        //�J�������[
        _edgeRight = nowStagePos.x + _CAMERAWIDE / 2;       //�J�����E�[
        _edgeUp = nowStagePos.y + _CAMERAHEIGHT / 2;        //�J������[
        _edgeDown = nowStagePos.y - _CAMERAHEIGHT / 2;      //�J�������[

        //�J�����X�V�A�J�����̒[�ɐG�ꂽ�Ƃ��J�����̍��W���X�V����
        //��������
        if (_edgeLeft >= transform.position.x)          //�E�̃X�e�[�W�ɍX�V
        {
            transform.position += _CAMERAWIDE * Vector3.right;
        }
        else if(_edgeRight <= transform.position.x)     //���̃X�e�[�W�ɍX�V
        {
            transform.position -= _CAMERAWIDE * Vector3.right;
        }

        if(_edgeDown >= transform.position.y)           //��̃X�e�[�W�ɍX�V
        {
            transform.position += _CAMERAHEIGHT * Vector3.up;
        }
        else if (_edgeUp <= transform.position.y)       //���̃X�e�[�W�ɍX�V
        {
            transform.position -= _CAMERAHEIGHT * Vector3.up;
        }
    
    }
    

    //��ֈāA_nowStage����J�����̈ʒu���X�V����
    
    void Update()
    {
        _edgeLeft = _nowStage.x - _CAMERAWIDE / 2;      //�J�������[
        _edgeRight = _nowStage.x + _CAMERAWIDE / 2;     //�J�����E�[
        _edgeUp = _nowStage.y + _CAMERAHEIGHT / 2;      //�J������[
        _edgeDown = _nowStage.y - _CAMERAHEIGHT / 2;    //�J�������[

        //�J�����X�V�A�J�����̒[�ɐG�ꂽ�Ƃ��J�����̍��W���X�V����
        //��������
        if (_edgeLeft >= transform.position.x)          //�E�̃X�e�[�W�ɍX�V
        {
            transform.position += _CAMERAWIDE * Vector3.right;
        }
        else if (_edgeRight <= transform.position.x)    //���̃X�e�[�W�ɍX�V
        {
            transform.position -= _CAMERAWIDE * Vector3.right;
        }

        //��������
        if(_edgeDown >= transform.position.y)           //��̃X�e�[�W�ɍX�V
        {
            transform.position += _CAMERAHEIGHT * Vector3.up;
        }
        else if (_edgeUp <= transform.position.y)       //���̃X�e�[�W�ɍX�V
        {
            transform.position -= _CAMERAHEIGHT * Vector3.up;
        }
    }
    
    
    
}
