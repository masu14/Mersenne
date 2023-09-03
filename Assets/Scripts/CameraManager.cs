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
    
    private const int cameraWidth = 18;                     //��ʉ���
    private const int cameraHeight = 10;                    //��ʏc��
    private float edgeRight, edgeLeft, edgeUp, edgeDown;    //�J�����ɉf��[�̍��W����                                   
    private Transform[] stages;                             //�V�[����̑S�ẴX�e�[�W���i�[
    private Vector2 _nowStage;                              //�v���C���[������X�e�[�W

    void Awake()
    {
        var _gameManager = FindObjectOfType<GameManager>();
        GameObject[] rawStages = GameObject.FindGameObjectsWithTag("Stage");
        stages = new Transform[rawStages.Length];
        StageManager[] stageManagers = FindObjectsOfType<StageManager>();

        for (int i = 0; i < rawStages.Length; i++)
        {
            stages[i] = rawStages[i].transform;
        }

        foreach (StageManager stageManager in stageManagers)
        {
            stageManager.OnPlayerEnter.Subscribe(_ => {
                //UpdateCameraPos(stageManager.transform.position)    �{���̓v���C���[�ƃX�e�[�W�̐ڐG�����m������J���������̃��\�b�h���Ăяo���̂��]�܂���
                _nowStage = stageManager.transform.position;        //��ֈāA�ڐG�����X�e�[�W��_nowStage�Ɋi�[
            } )
                .AddTo(this);
        }

        /*�o�O�Y���ӏ��A���[�h���ɑ��M�����Z�[�u�f�[�^�̍w�ǂ����鏈��
        _gameManager.OnSaveStage
            .ObserveOnMainThread()
            .Subscribe(x =>
            {
                UpdateCameraPos(x);
                Debug.Log($"���[�h���̃X�e�[�W�̍��W{x}");

            })
            .AddTo(this);
        */
    }


    /*  �J���������̃��\�b�h
    private void UpdateCameraPos(Vector2 nowStagePos)
    {
        edgeLeft = nowStagePos.x - cameraWidth / 2;
        edgeRight = nowStagePos.x + cameraWidth / 2;
        edgeUp = nowStagePos.y + cameraHeight / 2;
        edgeDown = nowStagePos.y - cameraHeight / 2;

        if(edgeLeft >= transform.position.x)
        {
            transform.position += cameraWidth * Vector3.right;
        }
        else if(edgeRight <= transform.position.x)
        {
            transform.position -= cameraWidth * Vector3.right;
        }

        if(edgeDown >= transform.position.y)
        {
            transform.position += cameraHeight * Vector3.up;
        }
        else if (edgeUp <= transform.position.y)
        {
            transform.position -= cameraHeight * Vector3.up;
        }
    
    }
    */

    //��ֈāA_nowStage����J�����̈ʒu���X�V����
    void Update()
    {
        edgeLeft = _nowStage.x - cameraWidth / 2;
        edgeRight = _nowStage.x + cameraWidth / 2;
        edgeUp = _nowStage.y + cameraHeight / 2;
        edgeDown = _nowStage.y - cameraHeight / 2;

        //�J�����X�V
        if (edgeLeft >= transform.position.x)
        {
            transform.position += cameraWidth * Vector3.right;
        }
        else if (edgeRight <= transform.position.x)
        {
            transform.position -= cameraWidth * Vector3.right;
        }

        if(edgeDown >= transform.position.y)
        {
            transform.position += cameraHeight * Vector3.up;
        }
        else if (edgeUp <= transform.position.y)
        {
            transform.position -= cameraHeight * Vector3.up;
        }
    }
    
}
