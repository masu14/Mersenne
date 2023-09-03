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
    
    private const int cameraWidth = 18;                     //画面横幅
    private const int cameraHeight = 10;                    //画面縦幅
    private float edgeRight, edgeLeft, edgeUp, edgeDown;    //カメラに映る端の座標成分                                   
    private Transform[] stages;                             //シーン上の全てのステージを格納
    private Vector2 _nowStage;                              //プレイヤーがいるステージ

    void Awake()
    {
        var _gameManager = FindObjectOfType<GameManager>();
        GameObject[] rawStages = GameObject.FindGameObjectsWithTag("Stage");
        stages = new Transform[rawStages.Length];
        StageManager[] stageManagers = FindObjectsOfType<StageManager>();

        for (int i = 0; i < rawStages.Length; i++)
        {
            stages[i] = rawStages[i].transform;
        }

        foreach (StageManager stageManager in stageManagers)
        {
            stageManager.OnPlayerEnter.Subscribe(_ => {
                //UpdateCameraPos(stageManager.transform.position)    本来はプレイヤーとステージの接触を検知したらカメラ処理のメソッドを呼び出すのが望ましい
                _nowStage = stageManager.transform.position;        //代替案、接触したステージを_nowStageに格納
            } )
                .AddTo(this);
        }

        /*バグ該当箇所、ロード時に送信されるセーブデータの購読をする処理
        _gameManager.OnSaveStage
            .ObserveOnMainThread()
            .Subscribe(x =>
            {
                UpdateCameraPos(x);
                Debug.Log($"ロード時のステージの座標{x}");

            })
            .AddTo(this);
        */
    }


    /*  カメラ処理のメソッド
    private void UpdateCameraPos(Vector2 nowStagePos)
    {
        edgeLeft = nowStagePos.x - cameraWidth / 2;
        edgeRight = nowStagePos.x + cameraWidth / 2;
        edgeUp = nowStagePos.y + cameraHeight / 2;
        edgeDown = nowStagePos.y - cameraHeight / 2;

        if(edgeLeft >= transform.position.x)
        {
            transform.position += cameraWidth * Vector3.right;
        }
        else if(edgeRight <= transform.position.x)
        {
            transform.position -= cameraWidth * Vector3.right;
        }

        if(edgeDown >= transform.position.y)
        {
            transform.position += cameraHeight * Vector3.up;
        }
        else if (edgeUp <= transform.position.y)
        {
            transform.position -= cameraHeight * Vector3.up;
        }
    
    }
    */

    //代替案、_nowStageからカメラの位置を更新する
    void Update()
    {
        edgeLeft = _nowStage.x - cameraWidth / 2;
        edgeRight = _nowStage.x + cameraWidth / 2;
        edgeUp = _nowStage.y + cameraHeight / 2;
        edgeDown = _nowStage.y - cameraHeight / 2;

        //カメラ更新
        if (edgeLeft >= transform.position.x)
        {
            transform.position += cameraWidth * Vector3.right;
        }
        else if (edgeRight <= transform.position.x)
        {
            transform.position -= cameraWidth * Vector3.right;
        }

        if(edgeDown >= transform.position.y)
        {
            transform.position += cameraHeight * Vector3.up;
        }
        else if (edgeUp <= transform.position.y)
        {
            transform.position -= cameraHeight * Vector3.up;
        }
    }
    
}
