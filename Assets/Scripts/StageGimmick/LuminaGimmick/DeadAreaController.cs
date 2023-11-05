using UnityEngine;

namespace Merusenne.StageGimmick.LuminaGimmick
{
    /// <summary>
    /// DeadArea(Barrier�̎q�I�u�W�F�N�g)�̃X�N���v�g�R���|�[�l���g
    /// �e��Barrier��active��Ԃ̂Ƃ��R���C�_�[��active�ɁA��active�̂Ƃ���active�ɂ���
    /// Barrier��active�ɂȂ����Ƃ���Barrier�Əd�Ȃ�ʒu�ɂ���ƃv���C���[��Dead����
    /// </summary>

    public class DeadAreaController : MonoBehaviour
    {
        private BoxCollider2D _myCollider;
        private BoxCollider2D _parentCollider;

        void Start()
        {
            _myCollider = GetComponent<BoxCollider2D>();                                //DeadArea�̃R���C�_�[
            _parentCollider = transform.parent.GetComponent<BoxCollider2D>();           //�e��Barrier�̃R���C�_�[

            SyncColliderState();        //�R���C�_�[�̏�Ԃ�������

        }

        void Update()
        {
            //�R���C�_�[�̏�Ԃ����낦��
            if (_myCollider.enabled != _parentCollider.enabled)
            {
                SyncColliderState();
            }
        }

        //DeadArea�̃R���C�_�[��active��Ԃ�e��Barrier�Ƃ��낦��
        void SyncColliderState()
        {
            Debug.Log("SyncColliderState");
            _myCollider.enabled = _parentCollider.enabled;
        }
    }

}
