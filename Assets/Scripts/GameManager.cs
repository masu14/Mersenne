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
    private SavePointController[] savePoints;

    string _filePath;
    SaveDataManager _save;
    
    private Vector2 playerPos;
    private Vector2 _playerStartPos = new Vector2(-5, 0);

    void Awake()
    {
        //プレイヤーがDeadしたときゲームリスタート
        _player = GameObject.FindWithTag("Player");
        _playerCore = _player.GetComponent<PlayerCore>();
        _playerCore.OnDead.Subscribe(_=>WaitGameRestart()).AddTo(this);


        _filePath = Application.dataPath + "/.savedata.json";
        _save = new SaveDataManager();

        //セーブポイントに触れたとき座標をセーブ
        savePoints = FindObjectsOfType<SavePointController>();
        foreach(var savePoint in savePoints)
        {
            savePoint.OnTriggerSave
                .Where(x => x != Vector2.zero)
                .Subscribe(x =>
            {
                _save._nowSavePos = x;
                
                Debug.Log($"セーブポイントの位置を変更しました:{x}");
            }).AddTo(this);
        }
        

    }

    private void Start()
    {
        Load();     //セーブデータのロード
        _player.transform.position = _save._nowSavePos;
        
    }
    /*
    void SaveGame(Vector2 data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(_savePath, jsonData);
    }*/


    public void Save()
    {
        
        Debug.Log($"セーブ時のnowSavePos:{_save._nowSavePos}");
        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(_filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
    }

    public void Load()
    {
        if (File.Exists(_filePath))
        {
            StreamReader streamReader = new StreamReader(_filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            _save = JsonUtility.FromJson<SaveDataManager>(data);
            //playerPos = _save._nowSavePos;
            Debug.Log($"ロード時のnowSavePos:{_save._nowSavePos}");
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
        Save();
        SceneManager.LoadScene(sceneName);
    }


}
