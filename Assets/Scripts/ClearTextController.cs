using UnityEngine;
using UniRx;
using TMPro;
using System;
using Merusenne.StageGimmick;

/// <summary>
/// ClearTextController��GameClear�I�u�W�F�N�g(�e�L�X�g)�ɕt����X�N���v�g�R���|�[�l���g
/// Goal�I�u�W�F�N�g�ɐG�ꂽ�Ƃ���莞�Ԍo�ߌ�A�uGame Clear�v�ƕ\������
/// </summary>

public class ClearTextController : MonoBehaviour
{
    private GameObject _goal;                   //�X�e�[�W���ɂ���S�[���I�u�W�F�N�g
    private GoalController _goalController;     //�S�[���I�u�W�F�N�g�̃X�N���v�g�R���|�[�l���g�A�S�[�����ɍw�ǂ���

    [SerializeField] private TextMeshProUGUI _clear_text;       //�uGame Clear�v�ƕ\������e�L�X�g�I�u�W�F�N�g
    [SerializeField] private float _wait_game_clear;            //Goal�I�u�W�F�N�g�ɐG��Ă���e�L�X�g��\������܂ł̎���

    private Subject<Unit> _onClear = new Subject<Unit>();       //�N���A�e�L�X�g��\�������Ƃ��ɑ��M
    public IObservable<Unit> OnClear => _onClear;               


    void Start()
    {
        _goal = GameObject.FindWithTag("Goal");                     
        _goalController = _goal.GetComponent<GoalController>();

        //�S�[�������Ƃ��ɑ��M���Ă���A�w�ǂ���ƈ�莞�Ԍo�ߌ�A�N���A�e�L�X�g��\������
        _goalController.OnGoal
        .Subscribe(_ =>WaitGameClear())
        .AddTo(this);

        _onClear.AddTo(this);

        _clear_text.alpha = 0.0f;       //�N���A�e�L�X�g�͏��ߓ����ɂ��Ă���
    }

    //�S�[���ʒm���͂��ƈ�莞�Ԍo�߂������GameClear()���\�b�h���Ă�
    void WaitGameClear()
    {
        Observable.Timer(TimeSpan.FromSeconds(_wait_game_clear)).Subscribe(_ => GameClear());
    }
    
    //�N���A�e�L�X�g��\������
    void GameClear()
    {
        _clear_text.alpha = 1.0f;
        _onClear.OnNext(Unit.Default);  //�e�L�X�g�̕\���𑗐M����
    }

}
