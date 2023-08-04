using Merusenne.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShotController : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private float shotSpeed = 0.02f;   //�����R�[�h�̑���
    [SerializeField] private float shotTime = 1.0f;     //�����R�[�h�̏��ł܂ł̎���
    private PlayerMove _playerMove;

    private bool _shotxDir;



    private void Start()
    {
        player = GameObject.FindWithTag("Player");                      //�v���C���[�I�u�W�F�N�g�擾
        _playerMove = player.GetComponent<PlayerMove>();     //�v���C���[�R���g���[���[�擾
    }
    private void Update()
    {
        _playerMove.Observable.Subscribe(xDir => _shotxDir = xDir);
        if (_shotxDir == true)                        //�X�v���C�g���E�����̂Ƃ�
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
