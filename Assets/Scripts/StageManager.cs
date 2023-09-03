using UnityEngine;
using UniRx;
using System;

public class StageManager : MonoBehaviour
{
    //public int stageNum;
    //[SerializeField] private CameraManager mainCamera;      //カメラ取得

    private Subject<Unit> playerEnter = new Subject<Unit>();

    public IObservable<Unit> OnPlayerEnter => playerEnter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーを検知したときnowStageを更新
        if(collision.gameObject.tag == "Player")
        {
            playerEnter.OnNext(Unit.Default);
            //mainCamera.nowStage = stageNum;
        }
    }
}
