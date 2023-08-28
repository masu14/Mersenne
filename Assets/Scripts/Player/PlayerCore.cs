using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class PlayerCore : MonoBehaviour
{
    private bool _isDead = false;                               //Deadフラグ
    private Subject<Unit> onDeadSubject = new Subject<Unit>();

    public bool IsDead => _isDead;
    public IObservable<Unit> OnDead => onDeadSubject;

    //Deadタグのオブジェクトに衝突したらDead状態になる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead"))
        {
            _isDead = true;
            onDeadSubject.OnNext(Unit.Default);
        }
    }

}
