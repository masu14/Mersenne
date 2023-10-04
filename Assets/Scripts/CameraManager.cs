using UnityEngine;
using UniRx;

/// <summary>
/// カメラの位置を制御するクラス、ステージごとの定点カメラでプレイヤーのいるステージを写す
/// 
/// 仕様：カメラはプレイヤーがステージのコライダーに接触したらそのステージを写す
///       ロード時に最後に触れたセーブポイントのあるステージを写す
///       
/// バグ：ロード時にGameManagerクラスから送信されるセーブデータ(セーブポイントのあるステージの座標)が購読されず、
///     　(x,y)=(cameraWidth,cameraHeight)のステージを写してしまう。
///     　
/// 代替案：Updateメソッドで毎フレーム、プレイヤーとステージの接触の検知を監視してカメラ処理を行う
///         この方法はStartメソッド時に行われないので、一瞬だけ(x,y)=(cameraWidth,cameraHeight)のステージを写してしまう。
///         またUpdateで呼ぶのは無駄の負荷をかけてしまう。
/// </summary>
/// 
public class CameraManager : MonoBehaviour
{
    
    private const int _CAMERAWIDE = 18;                     //画面横幅
    private const int _CAMERAHEIGHT = 10;                    //画面縦幅
    private float _edgeRight, _edgeLeft, _edgeUp, _edgeDown;    //カメラに映る端の座標成分                                   

    private Vector2 _nowStage;                              //プレイヤーがいるステージ

    void Awake()
    {
        var _gameManager = FindObjectOfType<GameManager>();

        //バグ該当箇所、ロード時に送信されるセーブデータの購読をする処理
        /*
        _gameManager.OnSaveStage
           .Subscribe(x =>
           {
               UpdateCameraPos(x);
               Debug.Log($"ロード時のステージの座標{x}");

           })
           .AddTo(this);
        */

        StageManager[] stageManagers = FindObjectsOfType<StageManager>();

        foreach (StageManager stageManager in stageManagers)
        {
            stageManager.OnPlayerEnter.Subscribe(pos => {
               // UpdateCameraPos(pos);    //本来はプレイヤーとステージの接触を検知したらカメラ処理のメソッドを呼び出すのが望ましい
                _nowStage = stageManager.transform.position;        //代替案、接触したステージを_nowStageに格納
            } )
                .AddTo(this);
        }


    }

    private void Start()
    {
        
    }

    //  カメラ処理のメソッド
    private void UpdateCameraPos(Vector2 nowStagePos)
    {
        _edgeLeft = nowStagePos.x - _CAMERAWIDE / 2;
        _edgeRight = nowStagePos.x + _CAMERAWIDE / 2;
        _edgeUp = nowStagePos.y + _CAMERAHEIGHT / 2;
        _edgeDown = nowStagePos.y - _CAMERAHEIGHT / 2;

        if(_edgeLeft >= transform.position.x)
        {
            transform.position += _CAMERAWIDE * Vector3.right;
        }
        else if(_edgeRight <= transform.position.x)
        {
            transform.position -= _CAMERAWIDE * Vector3.right;
        }

        if(_edgeDown >= transform.position.y)
        {
            transform.position += _CAMERAHEIGHT * Vector3.up;
        }
        else if (_edgeUp <= transform.position.y)
        {
            transform.position -= _CAMERAHEIGHT * Vector3.up;
        }
    
    }
    

    //代替案、_nowStageからカメラの位置を更新する
    
    void Update()
    {
        _edgeLeft = _nowStage.x - _CAMERAWIDE / 2;
        _edgeRight = _nowStage.x + _CAMERAWIDE / 2;
        _edgeUp = _nowStage.y + _CAMERAHEIGHT / 2;
        _edgeDown = _nowStage.y - _CAMERAHEIGHT / 2;

        //カメラ更新
        if (_edgeLeft >= transform.position.x)
        {
            transform.position += _CAMERAWIDE * Vector3.right;
        }
        else if (_edgeRight <= transform.position.x)
        {
            transform.position -= _CAMERAWIDE * Vector3.right;
        }

        if(_edgeDown >= transform.position.y)
        {
            transform.position += _CAMERAHEIGHT * Vector3.up;
        }
        else if (_edgeUp <= transform.position.y)
        {
            transform.position -= _CAMERAHEIGHT * Vector3.up;
        }
    }
    
    
    
}
