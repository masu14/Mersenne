using System;
using UnityEngine;

/// <summary>
/// �ۑ����Ă����f�[�^���Ǘ�����X�N���v�g
/// GameManager�N���X�Ńf�[�^�̓ǂݏ������s��json�`����Asset�t�H���_���ɕۑ�����
/// </summary>
[Serializable]
public class SaveDataManager 
{
    //�Z�[�u�f�[�^
    public Vector2 _nowSavePos;                    //�Z�[�u�|�C���g���W
    public Vector2 _nowStagePos;                   //�Z�[�u�|�C���g�̂���X�e�[�W�̍��W
    public bool _isFisrtPlay = false;
}
