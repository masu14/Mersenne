using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System;
using System.IO;

/// <summary>
/// ゲーム全体の管理を行うクラス
/// シーンの遷移、セーブ、ロードの制御を行う
/// </summary>
public class GameManager : MonoBehaviour
{
    
    private GameObject _player;
    private PlayerCore _playerCore;
    private SavePointController[] _savePoints;
    private SaveDataManager _save;
    
    //パラメータ
    [SerializeField] private float _loadWaitTime = 2.0f;                    //プレイヤーがDeadしてからロードされるまでの時間

    private string _sceneName = "StageScene";                               //ロードするシーン名
    private string _filePath;                                               //セーブデータの保存先
    private Vector2 _playerStartPos = new Vector2(-5, 0);                   //セーブデータがないときのプレイヤーの開始位置
    private Vector2 _playerPosUp = new Vector2(0, 2);                       //セーブポイント上空のプレイヤーの開始位置

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");                         //プレイヤー取得
        _playerCore = _player.GetComponent<PlayerCore>();                   //プレイヤーの状態取得
        _filePath = Application.dataPath + "/.savedata.json";               //セーブデータの保存先登録
        _save = new SaveDataManager();                                      //セーブデータの管理先取得
        _savePoints = FindObjectsOfType<SavePointController>();             //シーン上の全てセーブポイントを取得

        //セーブデータのロード
        Load();     

        //プレイヤーDead時に一定時間をおいてリロード、OnDestroy時にDispose()されるように登録
        _playerCore.OnDead.Subscribe(_ => WaitGameRestart()).AddTo(this);

        //全てのセーブポイントを購読
        foreach (var savePoint in _savePoints)
        {
            //セーブポイントに触れたときセーブデータを更新、OnDestroy時にDispose()されるように登録
            savePoint.OnTriggerSave
                .Subscribe(x =>
                {
                    _save._nowSavePos = x;

                    Debug.Log($"セーブポイントの位置を変更しました:{x}");
                }).AddTo(this);
        }

    }

    private void Start()
    {
        _player.transform.position = _save._nowSavePos + _playerPosUp;      //ロード時のプレイヤーの開始位置

    }
    //セーブデータを更新しセーブする
    public void Save()
    {
        
        Debug.Log($"セーブ時のnowSavePos:{_save._nowSavePos}");
        string json = JsonUtility.ToJson(_save);                    //セーブデータをjson文字列に変換
        StreamWriter streamWriter = new StreamWriter(_filePath);    //_filePathにjson文字列を保存するテキストファイル作成
        streamWriter.Write(json); streamWriter.Flush();             //json文字列の書き込み、書き込み操作の確定
        streamWriter.Close();                                       //ファイルを閉じて書き込み終了
    }

    //ゲーム開始時、プレイヤーDead時にロードする
    public void Load()
    {
        if (File.Exists(_filePath))     //ファイルが存在するとき
        {
            StreamReader streamReader = new StreamReader(_filePath);    //_filePathのjson文字列を読み込むファイル作成
            string data = streamReader.ReadToEnd();                     //ファイル全体を読み込みdataに格納
            streamReader.Close();                                       //ファイルを閉じて読み込み終了
            _save = JsonUtility.FromJson<SaveDataManager>(data);        //json文字列をセーブデータに変換
            Debug.Log($"ロード時のnowSavePos:{_save._nowSavePos}");
        }
        else                           //ファイルが存在しないとき
        {
            Debug.Log("セーブデータが見つかりません。新しいゲームを開始します。");
            _save._nowSavePos = _playerStartPos;                        //デフォルトのプレイヤーの開始位置を格納
        }
    }

    //プレイヤーDead時に一定時間待つ
    void WaitGameRestart()
    {
        Observable.Timer(TimeSpan.FromSeconds(_loadWaitTime)).Subscribe(_ => GameRestart());
    }

    //プレイヤーDead時に一定時間経過後、セーブデータをセーブした後リロードする
    private void GameRestart()
    {
        Save();
        SceneManager.LoadScene(_sceneName);
    }


}
