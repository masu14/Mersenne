using UniRx;

namespace Merusenne.Player
{
    //���͂��`����C���^�[�t�F�[�X�A�Q�Ƃ�����̓C�x���g�Ɉˑ������X�N���v�g�Ɉړ��ł���
    //���͂̍����ւ��͂�������s��
    public interface IInputEventProvider
    {

        IReadOnlyReactiveProperty<float> AxisH { get; }         //���������̓���
        IReadOnlyReactiveProperty<bool> IsJump { get; }         //�W�����v�̓���
        IReadOnlyReactiveProperty<bool> IsUpSwitch { get; }     //�V���b�g�؂�ւ��̓���
        IReadOnlyReactiveProperty<bool> IsShot { get; }         //�V���b�g���˂̓���
        IReadOnlyReactiveProperty<bool> IsThrough { get; }      //���蔲�������~������
    }
}