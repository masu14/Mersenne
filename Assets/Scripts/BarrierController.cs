using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    [SerializeField] private GimmickController gimmickController;               //Gimmickオブジェクト取得

    void FixedUpdate()
    {
        //Gimmickオブジェクトの色と同じとき消滅
        if (gimmickController.lightRed && gameObject.tag == "Barrier_red")      //赤に発光しているとき
        {
            Destroy(gameObject);
            gimmickController.lightRed = false;
        }

        if (gimmickController.lightBlue && gameObject.tag == "Barrier_blue")    //青に発光しているとき
        {
            Destroy(gameObject);
            gimmickController.lightBlue = false;
        }

        if (gimmickController.lightGreen && gameObject.tag == "Barrier_green")  //緑に発光しているとき
        {
            Destroy(gameObject);
            gimmickController.lightGreen = false;
        }
    }
}
