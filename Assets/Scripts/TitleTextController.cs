using System.Collections;
using System.Collections.Generic;
using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TitleTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] [Range(0.1f, 10.0f)] float duration = 1.0f;
    [SerializeField] private Color32 _startColor = new Color32(255, 255, 255, 255);
    [SerializeField] private Color32 _endColor = new Color32(255, 255, 255, 0);
    private float _time = 0;
    private string _sceneName = "StageScene";                               //ロードするシーン名

    [SerializeField] private float _startWaitTime = 2.0f;

    void Update()
    {
        _time += Time.deltaTime;
        _titleText.color = Color.Lerp(_startColor, _endColor, Mathf.PingPong(_time / duration, 1.0f)); 

        if(Input.anyKeyDown)
        {
            duration = 0.01f;
            Debug.Log("pressanybutton");
            WaitGameStart();
        }
    }

    private void WaitGameStart()
    {
        Debug.Log("waitgamestart");
       
        Observable.Timer(TimeSpan.FromSeconds(_startWaitTime)).Subscribe(_ => GameStart());
    }

    private void GameStart()
    {
        Debug.Log("gamestart");
        SceneManager.LoadScene(_sceneName);
    }
}
