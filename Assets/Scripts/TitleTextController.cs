using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// �^�C�g����ʂł̃e�L�X�g�̖��ÁA���́A�X�e�[�W�V�[���ւ̑J�ڂ̏����𐧌䂷��
/// �uPress Any Button�v�e�L�X�g�͖��Â�ω������ē_�ł�����B���͂�������Ɠ_�ł̃X�s�[�h�������Ȃ�A��莞��
/// �o�ߌ�X�e�[�W�V�[���֑J�ڂ���B
/// </summary>

public class TitleTextController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _title_text;                               //�^�C�g���V�[���̃e�L�X�g�A�_�ł���
    [SerializeField] [Range(0.1f, 10.0f)] float _duration = 1.0f;                       //�_�ł̊Ԋu
    [SerializeField] float _duration_before_change = 0.01f;                             //�X�e�[�W�V�[���ւ̑J�ڒ��O�̓_�ł̊Ԋu
    [SerializeField] private Color32 _start_color = new Color32(255, 255, 255, 255);    //���邢��ԁA��
    [SerializeField] private Color32 _end_color = new Color32(255, 255, 255, 0);        //�����ȏ�ԁA
    [SerializeField] private float _start_wait_time = 2.0f;                             //���͂����m���Ă���X�e�[�W�V�[���ɑJ�ڂ���܂ł̎���


    private float _time = 0;                    //���ԃp�����[�^�̏�����
    private string _sceneName = "StageScene";   //���[�h����V�[�����A�^�C�g�� -> �X�e�[�W

    void Update()
    {
        _time += Time.deltaTime;                                                                                //���ԃp�����[�^�𖈃t���[���X�V�A�_�ł̎��ԕω��Ɏg��
        _title_text.color = Color.Lerp(_start_color, _end_color, Mathf.PingPong(_time / _duration, 1.0f));      //�_�ŏ���


        //���͂��󂯎������X�e�[�W�V�[���ւ̑J�ڂ̏����Ɉڂ�
        if(Input.anyKeyDown)
        {
            _duration = _duration_before_change;      //�_�ŊԊu��Z������
            Debug.Log("pressanybutton");
            WaitGameStart();                          //��莞�Ԍo�ߌ�X�e�[�W�V�[����  
        }
    }

    //�X�e�[�W�V�[���֑J�ڂ���O�Ɉ�莞�ԑ҂�
    private void WaitGameStart()
    {
        Debug.Log("waitgamestart");
        Observable.Timer(TimeSpan.FromSeconds(_start_wait_time)).Subscribe(_ => GameStart());   //_start_wait_time�o�ߌ�GameStart()��
    }

    //�X�e�[�W�V�[���ւ̑J��
    private void GameStart()
    {
        Debug.Log("gamestart");
        SceneManager.LoadScene(_sceneName); //�X�e�[�W�V�[�������[�h
    }
}
