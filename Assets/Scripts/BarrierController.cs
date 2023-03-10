using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class BarrierController : MonoBehaviour
{
    [SerializeField] private GameObject targetGimmickController;               //Gimmick�I�u�W�F�N�g�擾
    private GimmickController gimmickController;
    [SerializeField] private Light2D freeFormLight;
    BoxCollider2D boxCollider2D;

                                             //Barrier�I�u�W�F�N�g�����t���O

    Color32 colorRed = new Color32(231, 69, 69, 255);
    Color32 colorBlue = new Color32(127, 255, 255, 255);
    Color32 colorGreen = new Color32(56, 241, 104, 255);



    private void Start()
    {
        gimmickController = targetGimmickController.GetComponent<GimmickController>();
        boxCollider2D = GetComponent<BoxCollider2D>();                            //BoxCollieder2D�擾
        
        

    }

    void Update()
    {
        
        
        //Gimmick�I�u�W�F�N�g�̐F�Ɠ����Ƃ�����
        if (gimmickController.shotCollision && gameObject.tag == "Barrier_red")      //�Ԃɔ������Ă���Ƃ�
        {
            SwitchBarrierLight(gimmickController.lightRed, colorRed);
            
        }

        if (gimmickController.shotCollision && gameObject.tag == "Barrier_blue")    //�ɔ������Ă���Ƃ�
        {
            SwitchBarrierLight(gimmickController.lightBlue, colorBlue);
        }

        if (gimmickController.shotCollision && gameObject.tag == "Barrier_green")    //�΂ɔ������Ă���Ƃ�
        {
            SwitchBarrierLight(gimmickController.lightGreen, colorGreen);
        }
        
        
    }

    public void SwitchCallBlue()
    {
        if(gameObject.tag == "Barrier_blue")
        {
            SwitchBarrierLight(gimmickController.lightBlue, colorBlue);
        }

    }

    public void SwitchCallGreen()
    {
        if(gameObject.tag == "Barrier_green")
        {
            SwitchBarrierLight(gimmickController.lightGreen, colorGreen);
        }
        
    }

    public void SwitchCallRed()
    {
        if(gameObject.tag == "Barrier_red")
        {
            SwitchBarrierLight(gimmickController.lightRed, colorRed);
        }
        
    }


    private void SwitchBarrierLight(bool barrierLight, Color32 color)
    {
        if(barrierLight)
        {
            boxCollider2D.enabled = false;
            freeFormLight.color = new Color32(0, 0, 0, 0);
            
        }
        else
        {
            boxCollider2D.enabled = true;
            freeFormLight.color = color;
            
        }
        
    }
}
