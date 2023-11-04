using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using Merusenne.StageGimmick;

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

    private GameObject _clearText;
    private ClearTextController _clearTextController;

    [SerializeField] GameObject _tutorial_action;
    
    //パラメータ
    [SerializeField] private float _load_wait_time = 2.0f;                  //プレイヤーがDeadしてからロードされるまでの時間
    [SerializeField] private float _pop_up_wait_time = 0.5f;                //ゲーム開始からチュートリアル表示までの時間

    private string _sceneName = "StageScene";                               //ロードするシーン名
    private string _filePath;                                               //セーブデータの保存先
    private Vector2 _playerStartPos = new Vector2(-5.5f, -4);                   //セーブデータがないときのプレイヤーの開始位置
    private Vector2 _playerPosUp = new Vector2(0, 0.5f);                       //セーブポイント上空のプレイヤーの開始出現位置
    private bool _isClear = false;
    private bool _isTutorial = false;                                       //チュートリアルが開いている状態
    

    private Subject<Vector2> _saveStage = new Subject<Vector2>();
    public IObservable<Vector2> OnSaveStage => _saveStage;
    void Awake()
    {
        _player = GameObject.FindWithTag("Player");                         //プレイヤー取得
        _playerCore = _player.GetComponent<PlayerCore>();                   //プレイヤーの状態取得
        _filePath = Application.dataPath + "/.savedata.json";               //セーブデータの保存先登録
        _save = new SaveDataManager();                                      //セーブデータの管理先取得
        _savePoints = FindObjectsOfType<SavePointController>();             //シーン上の全てセーブポイントを取得
        _clearText = GameObject.FindWithTag("ClearText");
        _clearTextController = _clearText.GetComponent<ClearTextController>();

        _sceneName = "StageScene";
       
        Debug.Log($"ロード時のnowStagePos:{_save._nowStagePos}");


        //プレイヤーDead時に一定時間をおいてリロード、OnDestroy時にDispose()されるように登録
        _playerCore.OnDead
            .Subscribe(_ => WaitGameRestart())
            .AddTo(this);

        //全てのセーブポイントを購読
        foreach (var savePoint in _savePoints)
        {
            //セーブポイントに触れたときセーブデータを更新、OnDestroy時にDispose()されるように登録
            savePoint.OnTriggerSave
                .Subscribe(x =>
                {
                    _save._nowSavePos = x;

                    Debug.Log($"セーブポイントの位置を変更しました:{x}");
                    Save();
                })
                .AddTo(this);

            savePoint.OnTriggerStage
                .Subscribe(x =>
                {
                    _save._nowStagePos = x;
                    Debug.Log($"セーブポイントのあるステージの位置を変更しました{x}");
                    Save();
                })
                .AddTo(this);
        }

        _clearTextController.OnClear
            .Subscribe(_ => _isClear = true)
            .AddTo(this);

        //セーブデータのロード
        Load();
        _saveStage.OnNext(_save._nowStagePos);                              //ロード時のステージの送信、CameraManagerが購読
       
    }

    private void Start()
    {
       
        _player.transform.position = _save._nowSavePos + _playerPosUp;      //ロード時のプレイヤーの開始位置

        if (!_save._isFisrtPlay)
        {
            _save._isFisrtPlay = true;
            Observable.Timer(TimeSpan.FromSeconds(_pop_up_wait_time))
                .Subscribe(_ =>
                {
                    _isTutorial = true;
                    _tutorial_action.GetComponent<PopUpController>().Open();
                    Debug.Log("ゲーム初プレイにつき、操作チュートリアルを表示します");
                })
                .AddTo(this);
        }

    }

    private void Update()
    {
        if (_isClear)
        {
            if (Input.anyKeyDown)
            {
                _sceneName = "TitleScene";
                _save._nowSavePos = _playerStartPos;
                Save();
                SceneManager.LoadScene(_sceneName);
            }
        }

        if (_isTutorial)
        {
            if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.D))
            {
                _isTutorial = false;
                _tutorial_action.GetComponent<PopUpController>().Close();
            }
        }
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
        Observable.Timer(TimeSpan.FromSeconds(_load_wait_time)).Subscribe(_ => GameRestart());
    }

    //プレイヤーDead時に一定時間経過後、セーブデータをセーブした後リロードする
    private void GameRestart()
    {
        
        SceneManager.LoadScene(_sceneName);
    }


}
