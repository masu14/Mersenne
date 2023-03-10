using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class BarrierController : MonoBehaviour
{
    [SerializeField] private GameObject targetGimmickController;               //Gimmickオブジェクト取得
    private GimmickController gimmickController;
    [SerializeField] private Light2D freeFormLight;
    BoxCollider2D boxCollider2D;

                                             //Barrierオブジェクト発光フラグ

    Color32 colorRed = new Color32(231, 69, 69, 255);
    Color32 colorBlue = new Color32(127, 255, 255, 255);
    Color32 colorGreen = new Color32(56, 241, 104, 255);



    private void Start()
    {
        gimmickController = targetGimmickController.GetComponent<GimmickController>();
        boxCollider2D = GetComponent<BoxCollider2D>();                            //BoxCollieder2D取得
        
        

    }

    void Update()
    {
        
        
        //Gimmickオブジェクトの色と同じとき消滅
        if (gimmickController.shotCollision && gameObject.tag == "Barrier_red")      //赤に発光しているとき
        {
            SwitchBarrierLight(gimmickController.lightRed, colorRed);
            
        }

        if (gimmickController.shotCollision && gameObject.tag == "Barrier_blue")    //青に発光しているとき
        {
            SwitchBarrierLight(gimmickController.lightBlue, colorBlue);
        }

        if (gimmickController.shotCollision && gameObject.tag == "Barrier_green")    //緑に発光しているとき
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
