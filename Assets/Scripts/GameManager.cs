using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _loadWaitTime = 2.0f;                      //プレイヤーがDeadしてからロードされるまでの時間

    private string sceneName = "StageScene";
    private GameObject _player;
    private PlayerCore _playerCore;
    private SavePointController[] savePoints;

    string _filePath;
    SaveDataManager _save;
    
    private Vector2 playerPos;
    private Vector2 _playerStartPos = new Vector2(-5, 0);
    private Vector2 _playerPosUp = new Vector2(0, 2);

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
        Load();     //セーブデータのロード

    }

    private void Start()
    {
        
        _player.transform.position = _save._nowSavePos + _playerPosUp;

    }
    
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
        Observable.Timer(TimeSpan.FromSeconds(_loadWaitTime)).Subscribe(_ => GameRestart());
    }
    private void GameRestart()
    {
        Save();
        SceneManager.LoadScene(sceneName);
    }


}
