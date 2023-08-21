using System;
using UnityEngine;
using System.IO;
using UniRx;

[Serializable]
class SaveDataManager : MonoBehaviour
{
    private GameObject _savePoint;
    private SavePointController _savePointController;

    //セーブデータ
    public Vector2 _nowSavePos;                    //セーブポイント座標


    private IDisposable _subSavePos;          //購読を解除するための変数
    private IDisposable _subNowSavePos;

    private ReactiveProperty<Vector2> savePos = new ReactiveProperty<Vector2>();
    public IReadOnlyReactiveProperty<Vector2> SavePosition => savePos;
    

    void Start()
    {
        //プレイヤーがセーブポイントに触れたらセーブポイントを更新
        _savePoint = GameObject.FindWithTag("SavePoint");
        _savePointController = _savePoint.GetComponent<SavePointController>();
        _subSavePos =　_savePointController.OnTrigger.Subscribe(x => savePos.Value = x);
        _subNowSavePos = _savePointController.OnTrigger.Subscribe(x => _nowSavePos = x);
    }


    private void OnDestroy()
    {
        //オブジェクトが破棄されるときに購読を解除
        _subSavePos.Dispose();
        _subNowSavePos.Dispose();
    }
}
