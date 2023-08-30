using UnityEngine;
using UniRx;
using System;

/// <summary>
/// �v���C���[�̏�Ԃ��Ǘ�����N���X
/// </summary>
public class PlayerCore : MonoBehaviour
{                              
    private readonly ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>(false);        //Dead�t���O
    private Subject<Unit> onDeadSubject = new Subject<Unit>();                                  //Dead�ʒm

    public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
    public IObservable<Unit> OnDead => onDeadSubject;

    void Start()
    {
        //Dead�ʒm�𑗐M
        _isDead.AddTo(this);
    }

    //Dead�^�O�̃I�u�W�F�N�g�ɏՓ˂�����Dead��ԂɂȂ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead"))
        {
            _isDead.Value = true;
            onDeadSubject.OnNext(Unit.Default);
        }
    }

}
