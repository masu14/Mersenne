using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    float waitTime = 2.0f;
    private string sceneName = "StageScene";
    private GameObject _player;
    private PlayerCore _playerCore;

    void Awake()
    {
        
        _player = GameObject.FindWithTag("Player");
        _playerCore = _player.GetComponent<PlayerCore>();

        _playerCore.OnDead.Subscribe(_=>WaitGameRestart()).AddTo(this);


    }


    void WaitGameRestart()
    {
        Observable.Timer(TimeSpan.FromSeconds(waitTime)).Subscribe(_ => GameRestart());
    }
    private void GameRestart()
    {
        
        SceneManager.LoadScene(sceneName);
    }


}
