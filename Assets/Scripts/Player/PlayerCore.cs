using UnityEngine;
using UniRx;
using System;

/// <summary>
/// プレイヤーの状態を管理するクラス
/// </summary>
public class PlayerCore : MonoBehaviour
{                              
    private readonly ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>(false);        //Deadフラグ
    private Subject<Unit> onDeadSubject = new Subject<Unit>();                                  //Dead通知

    public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
    public IObservable<Unit> OnDead => onDeadSubject;

    void Start()
    {
        //Dead通知を送信
        _isDead.AddTo(this);
    }

    //Deadタグのオブジェクトに衝突したらDead状態になる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead"))
        {
            _isDead.Value = true;
            onDeadSubject.OnNext(Unit.Default);
        }
    }

}
