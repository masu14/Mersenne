using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    float waitTime = 2.0f;
    private string sceneName = "StageScene";
    private GameObject _player;
    private PlayerCore _playerCore;
    private GameObject _saveDataObj;
    private SaveDataManager _saveData;
    private Vector2 playerPos;

    private const string _savePath = "save.json";
    private Vector2 _playerStartPos = new Vector2(-5,0);

    //private ReactiveProperty<Vector2> _playerPos = new ReactiveProperty<Vector2>();
    //public IReadOnlyReactiveProperty<Vector2> PlayerPosition => _playerPos;

    void Awake()
    {
        //プレイヤーがDeadしたときゲームリスタート
        _player = GameObject.FindWithTag("Player");
        _playerCore = _player.GetComponent<PlayerCore>();
        _playerCore.OnDead.Subscribe(_=>WaitGameRestart()).AddTo(this);

        //セーブデータ更新
        _saveDataObj = GameObject.FindWithTag("SaveDataManager");
        _saveData = _saveDataObj.GetComponent<SaveDataManager>();
        _saveData.SavePosition.Subscribe(x => SaveGame(x)).AddTo(this);

        
    }

    private void Start()
    {
        LoadGame();     //セーブデータのロード
        _player.transform.position = playerPos;
    }

    void SaveGame(Vector2 data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(_savePath, jsonData);
    }

    void LoadGame()
    {
        if (File.Exists(_savePath))
        {
            string jsonData = File.ReadAllText(_savePath);
            SaveDataManager data = JsonUtility.FromJson<SaveDataManager>(jsonData);
            playerPos = data._nowSavePos;
            
        }
        else
        {
            Debug.Log("セーブデータが見つかりません。新しいゲームを開始します。");
            playerPos = _playerStartPos;
        }
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
