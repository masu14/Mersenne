using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stageNum;
    [SerializeField] private CameraManager mainCamera;      //�J�����擾
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�����m�����Ƃ�nowStage���X�V
        if(collision.gameObject.tag == "Player")
        {
            mainCamera.nowStage = stageNum;
        }
    }
}
