using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private const int stageMaxnum = 100;                    //�X�e�[�W���̍ő�l
    private const int cameraWidth = 18;                     //��ʉ���
    private const int cameraHeight = 10;                    //��ʏc��
    public int nowStage;                                    //�v���C���[�̂���X�e�[�W�ԍ�(nowStage�ɑΉ�)
    private float edgeRight, edgeLeft, edgeUp, EdgeDown;
    private int tmp;
    private GameObject[] rawStages;
    private GameObject[] stages;

    // Start is called before the first frame update
    void Start()
    {
        rawStages = GameObject.FindGameObjectsWithTag("Stage");

        stages = new GameObject[stageMaxnum];

        for (int i = 0; i < rawStages.Length; i++)
        {
            tmp = rawStages[i].GetComponent<StageManager>().stageNum;

            stages[tmp] = rawStages[i];

        }

        
    }

    
    void Update()
    {
        edgeLeft = stages[nowStage].transform.position.x - cameraWidth / 2;
        edgeRight = stages[nowStage].transform.position.x + cameraWidth / 2;
        edgeUp = stages[nowStage].transform.position.y + cameraHeight / 2;
        EdgeDown = stages[nowStage].transform.position.y - cameraHeight / 2;

        if (edgeLeft >= transform.position.x)
        {
            transform.position += cameraWidth * Vector3.right;
        }
        else if (edgeRight <= transform.position.x)
        {
            transform.position -= cameraWidth * Vector3.right;
        }

        if(EdgeDown >= transform.position.y)
        {
            transform.position += cameraHeight * Vector3.up;
        }
        else if (edgeUp <= transform.position.y)
        {
            transform.position -= cameraHeight * Vector3.up;
        }
    }
}
