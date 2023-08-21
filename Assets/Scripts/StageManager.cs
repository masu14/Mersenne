using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stageNum;
    [SerializeField] private CameraManager mainCamera;      //カメラ取得
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーを検知したときnowStageを更新
        if(collision.gameObject.tag == "Player")
        {
            mainCamera.nowStage = stageNum;
        }
    }
}
