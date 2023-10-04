using UniRx;

namespace Merusenne.Player
{
    /// <summary>
    ///���͂��`����C���^�[�t�F�[�X�A�Q�Ƃ�����̓C�x���g�Ɉˑ������X�N���v�g�Ɉړ��ł���
    ///���͂̒ǉ��A�폜�͂�������s��
    /// </summary>

    public interface IInputEventProvider
    {

        IReadOnlyReactiveProperty<float> AxisH { get; }         //���������̓���
        IReadOnlyReactiveProperty<bool> IsJump { get; }         //�W�����v�̓���
        IReadOnlyReactiveProperty<bool> IsLeftSwitch { get; }   //�V���b�g�؂�ւ�(��)�̓���
        IReadOnlyReactiveProperty<bool> IsRightSwitch { get; }  //�V���b�g�؂�ւ�(�E)�̓���
        IReadOnlyReactiveProperty<bool> IsShot { get; }         //�V���b�g���˂̓���
        IReadOnlyReactiveProperty<bool> IsThrough { get; }      //���蔲�������~������
    }
}