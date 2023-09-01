using System;
using UnityEngine;

/// <summary>
/// 保存しておくデータを管理するスクリプト
/// GameManagerクラスでデータの読み書きを行いjson形式でAssetフォルダ内に保存する
/// </summary>
[Serializable]
public class SaveDataManager 
{
    //セーブデータ
    public Vector2 _nowSavePos;                    //セーブポイント座標
}
