using UnityEngine;

/// <summary>
/// tutorial�I�u�W�F�N�g�ɕt����X�N���v�g�R���|�[�l���g
/// PopUpScaleController�̃T�C�Y�ύX�̏�����������Open,Close�̃p�����[�^���C���X�y�N�^�[�ŕύX�ł���
/// �|�b�v�A�b�v�����̓Q�[�����v���C����GameManager���C�΂̏��ɐG�ꂽ�Ƃ�TutorialSpotController�����ꂼ��Ăяo��
/// </summary>

public class PopUpController : MonoBehaviour
{
   
    public PopUpScaleController open, close;        //���ꂼ��̃p�����[�^���`����Play()�ŃT�C�Y�ύX

    void Start()
    {
        open.Setup(gameObject);     //open�Ƀ|�b�v�A�b�v�摜�̃I�u�W�F�N�g���Z�b�g����
        close.Setup(gameObject);    //close�Ƀ|�b�v�A�b�v�摜�̃I�u�W�F�N�g���Z�b�g����
    }

    //�|�b�v�A�b�v���J������
    public void Open()
    {
        open.Play();
    }

    //�|�b�v�A�b�v�����鏈��
    public void Close()
    {
        close.Play();
    }

}