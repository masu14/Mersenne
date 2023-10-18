using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// タイトル画面でのテキストの明暗、入力、ステージシーンへの遷移の処理を制御する
/// </summary>

public class TitleTextController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _title_text;                               //タイトルシーンのテキスト、点滅する
    [SerializeField] [Range(0.1f, 10.0f)] float _duration = 1.0f;                       //点滅の間隔
    [SerializeField] float _duration_before_change = 0.01f;                             //ステージシーンへの遷移直前の点滅の間隔
    [SerializeField] private Color32 _start_color = new Color32(255, 255, 255, 255);    //明るい状態、白
    [SerializeField] private Color32 _end_color = new Color32(255, 255, 255, 0);        //透明な状態、
    [SerializeField] private float _start_wait_time = 2.0f;                             //入力を検知してからステージシーンに遷移するまでの時間


    private float _time = 0;                    //時間パラメータの初期化
    private string _sceneName = "StageScene";   //ロードするシーン名、タイトル -> ステージ

    void Update()
    {
        _time += Time.deltaTime;                                                                                //時間パラメータを毎フレーム更新
        _title_text.color = Color.Lerp(_start_color, _end_color, Mathf.PingPong(_time / _duration, 1.0f));      //点滅処理


        //入力を受け取ったらステージシーンへの遷移の処理に移る
        if(Input.anyKeyDown)
        {
            _duration = _duration_before_change;      //点滅間隔を短くする
            Debug.Log("pressanybutton");
            WaitGameStart();                          //一定時間経過後ステージシーンへ  
        }
    }

    //ステージシーンへ遷移する前に一定時間待つ
    private void WaitGameStart()
    {
        Debug.Log("waitgamestart");
        Observable.Timer(TimeSpan.FromSeconds(_start_wait_time)).Subscribe(_ => GameStart());   //一定時間経過後GameStartへ
    }

    //ステージシーンへの遷移
    private void GameStart()
    {
        Debug.Log("gamestart");
        SceneManager.LoadScene(_sceneName); //ステージシーンをロード
    }
}
