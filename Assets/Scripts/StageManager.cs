using UnityEngine;
using UniRx;
using System;

/// <summary>
/// 
/// �X�e�[�W�̈ʒu��CameraManager�ɑ��M���邽�߂̃N���X
/// �v���C���[������X�e�[�W�̍��W��CameraManager��_nowStage�ɋL�^���A�J�����̍X�V�Ɏg��
/// 
/// </summary>

public class StageManager : MonoBehaviour
{
    private Subject<Vector2> _playerEnter = new Subject<Vector2>();     //�v���C���[�̐ڐG�����X�e�[�W�̍��W

    public IObservable<Vector2> OnPlayerEnter => _playerEnter;          //�v���C���[���X�e�[�W�̃R���C�_�[�ɐڐG�����^�C�~���O�ŃX�e�[�W���W�𑗐M


    //�v���C���[�̐ڐG���m
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�����m�����Ƃ�nowStage���X�V
        if(collision.gameObject.tag == "Player")
        {
            _playerEnter.OnNext(transform.position);
        }
    }
}
