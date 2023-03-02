using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    [SerializeField] private float shotSpeed = 0.02f;   //�����R�[�h�̑���
    [SerializeField] private float shotTime = 1.0f;     //�����R�[�h�̏��ł܂ł̎���

    

    private void Start()
    {
        player = GameObject.FindWithTag("Player");                      //�v���C���[�I�u�W�F�N�g�擾
        playerController = player.GetComponent<PlayerController>();     //�v���C���[�R���g���[���[�擾
    }
    private void Update()
    {
        if(playerController.playerRight == true)                        //�X�v���C�g���E�����̂Ƃ�
        {
            ShotMove(shotSpeed);
            
        }
        else                                                            //�X�v���C�g���������̂Ƃ�
        {
            ShotMove(-shotSpeed);
            
        }
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, shotTime);                                      //��莞�Ԍo�ߌ�����R�[�h����
    }

    private void ShotMove(float shotSpeed)                             //�����R�[�h�̈ړ�
    {
        transform.Translate(shotSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�ڐG");
        Destroy(gameObject);
    }
}
