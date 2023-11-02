using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using System;
using Merusenne.StageGimmick;

public class ClearTextController : MonoBehaviour
{
    private GameObject _goal;
    private GoalController _goalController;

    [SerializeField] private TextMeshProUGUI _clear_title;
    [SerializeField] private float _wait_game_clear;

    private Subject<Unit> _onClear = new Subject<Unit>();
    public IObservable<Unit> OnClear => _onClear;


    void Start()
    {
        _goal = GameObject.FindWithTag("Goal");
        _goalController = _goal.GetComponent<GoalController>();

        _goalController.OnGoal
        .Subscribe(_ =>WaitGameClear())
        .AddTo(this);

        _onClear.AddTo(this);

        _clear_title.alpha = 0.0f;
    }

    void WaitGameClear()
    {
        Observable.Timer(TimeSpan.FromSeconds(_wait_game_clear)).Subscribe(_ => GameClear());
    }
    
    void GameClear()
    {
        _clear_title.alpha = 1.0f;
        _onClear.OnNext(Unit.Default);
    }

    void Update()
    {
        
    }
}
