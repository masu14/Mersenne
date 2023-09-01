using UnityEngine;
using UnityEngine.Rendering.Universal;
using UniRx;
using System;

/// <summary>
/// セーブポイントの制御をするクラス
/// プレイヤーがセーブポイントに触れたら、次回ロード時プレイヤーはその位置からゲームを開始する
/// </summary>
public class SavePointController : MonoBehaviour
{
    private Light2D _saveLight;             //子オブジェクトのLight
    private Vector2 _savePointPos;          //セーブポイントの位置

    //最後に触れたセーブポイントの位置を保持
    private Subject<Vector2> _savePoint = new Subject<Vector2>();

    //最後に触れたセーブポイントの位置を送信
    public IObservable<Vector2> OnTriggerSave => _savePoint;
    void Start()
    {
        _saveLight = transform.GetChild(0).GetComponent<Light2D>();     //子オブジェクトのLight取得
        _savePointPos = transform.position;                             //自身の位置をセーブポイントに登録

        //OnDestroy時にDispose()されるように登録
        _savePoint.AddTo(this);
    }

    //セーブポイントに触れたら更新
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")    
        {
            _saveLight.color = new Color32(252, 252, 252, 252);     //セーブポイントの色が変わる
            Debug.Log("savepoint");
            _savePoint.OnNext(_savePointPos);
        }
    }
}
