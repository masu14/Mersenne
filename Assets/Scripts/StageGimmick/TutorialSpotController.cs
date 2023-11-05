using UnityEngine;


namespace Merusenne.StageGimmick
{
    /// <summary>
    /// TutorialSpot�ɕt����X�N���v�g�R���|�[�l���g
    /// �v���C���[��TutorialSpot(�ΐF�̏�)�̃R���C�_�[�ɐG�ꂽ�Ƃ��Ƀ`���[�g���A����\���A���ꂽ�Ƃ��Ƀ`���[�g���A�������
    /// </summary>

    public class TutorialSpotController : MonoBehaviour
    {
        [SerializeField] private GameObject _tutorial;      //�\������`���[�g���A���摜�̃I�u�W�F�N�g��ݒ肷��

        //�ΐF�̏��ɐG�ꂽ�Ƃ��`���[�g���A�����J��
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _tutorial.GetComponent<PopUpController>().Open();
            }
        }

        //�ΐF�̏����痣�ꂽ�Ƃ��`���[�g���A�������
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _tutorial.GetComponent<PopUpController>().Close();
            }
        }
    }

}
