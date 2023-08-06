using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class PlayerCore : MonoBehaviour
{
    private bool _isDead = false;
    private Subject<Unit> onDeadSubject = new Subject<Unit>();

    public bool IsDead => _isDead;
    public IObservable<Unit> OnDead => onDeadSubject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead"))
        {
            _isDead = true;
            onDeadSubject.OnNext(Unit.Default);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
