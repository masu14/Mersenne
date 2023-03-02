using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    [SerializeField] private GimmickController gimmickController;               //Gimmick�I�u�W�F�N�g�擾

    void FixedUpdate()
    {
        //Gimmick�I�u�W�F�N�g�̐F�Ɠ����Ƃ�����
        if (gimmickController.lightRed && gameObject.tag == "Barrier_red")      //�Ԃɔ������Ă���Ƃ�
        {
            Destroy(gameObject);
            gimmickController.lightRed = false;
        }

        if (gimmickController.lightBlue && gameObject.tag == "Barrier_blue")    //�ɔ������Ă���Ƃ�
        {
            Destroy(gameObject);
            gimmickController.lightBlue = false;
        }

        if (gimmickController.lightGreen && gameObject.tag == "Barrier_green")  //�΂ɔ������Ă���Ƃ�
        {
            Destroy(gameObject);
            gimmickController.lightGreen = false;
        }
    }
}
