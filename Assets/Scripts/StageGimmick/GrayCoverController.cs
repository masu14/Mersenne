using UnityEngine;

namespace Merusenne.StageGimmick
{

    /// <summary>
    /// �X�e�[�W�̉B���ʘH�p�̃J�o�[�𐧌䂷��N���X
    /// </summary>
    public class GrayCoverController : MonoBehaviour
    {

        //�G�ꂽ�������
        private void OnTriggerEnter2D(Collider2D collider)
        {
            Destroy(gameObject);
        }
    }

}
