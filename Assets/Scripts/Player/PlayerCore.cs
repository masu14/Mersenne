using UnityEngine;
using UniRx;
using System;

/// <summary>
/// �v���C���[�̏�Ԃ��Ǘ�����N���X
/// �v���C���[��Dead�^�O�����I�u�W�F�N�g�ɐڐG����ƁADead��ԂɂȂ�
/// </summary>
public class PlayerCore : MonoBehaviour
{                              
    private readonly ReactiveProperty<bool> _isDead = new ReactiveProperty<bool>(false);        //Dead�t���O
    private Subject<Unit> _onDead = new Subject<Unit>();                                        //Dead�ʒm

    //Dead��Ԃ𑗐M
    public IReadOnlyReactiveProperty<bool> IsDead => _isDead;
    public IObservable<Unit> OnDead => _onDead;

    void Start()
    {
        //OnDestroy����Dispose()�����悤�ɓo�^
        _isDead.AddTo(this);
        _onDead.AddTo(this);
    }

    //Dead�^�O�̃I�u�W�F�N�g�ɏՓ˂�����Dead��ԂɂȂ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead"))
        {
            _isDead.Value = true;
            _onDead.OnNext(Unit.Default);
        }
    }


}
