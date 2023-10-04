using UnityEngine;
using UniRx;
using System;

public class StageManager : MonoBehaviour
{
    private Subject<Vector2> _playerEnter = new Subject<Vector2>();

    public IObservable<Vector2> OnPlayerEnter => _playerEnter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�����m�����Ƃ�nowStage���X�V
        if(collision.gameObject.tag == "Player")
        {
            _playerEnter.OnNext(transform.position);
        }
    }
}
