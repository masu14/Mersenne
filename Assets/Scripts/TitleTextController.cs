using System.Collections;
using System.Collections.Generic;
using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TitleTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title_text;
    [SerializeField] [Range(0.1f, 10.0f)] float _duration = 1.0f;
    [SerializeField] private Color32 _start_color = new Color32(255, 255, 255, 255);
    [SerializeField] private Color32 _end_color = new Color32(255, 255, 255, 0);
    private float _time = 0;
    private string _sceneName = "StageScene";                               //ロードするシーン名

    [SerializeField] private float _start_wait_time = 2.0f;

    void Update()
    {
        _time += Time.deltaTime;
        _title_text.color = Color.Lerp(_start_color, _end_color, Mathf.PingPong(_time / _duration, 1.0f)); 

        if(Input.anyKeyDown)
        {
            _duration = 0.01f;
            Debug.Log("pressanybutton");
            WaitGameStart();
        }
    }

    private void WaitGameStart()
    {
        Debug.Log("waitgamestart");
       
        Observable.Timer(TimeSpan.FromSeconds(_start_wait_time)).Subscribe(_ => GameStart());
    }

    private void GameStart()
    {
        Debug.Log("gamestart");
        SceneManager.LoadScene(_sceneName);
    }
}
