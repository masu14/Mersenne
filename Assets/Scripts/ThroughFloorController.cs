using Merusenne.Player.InputImpls;
using UnityEngine;
using System;
using UniRx;


public class ThroughFloorController : MonoBehaviour
{
    private BoxCollider2D _myCollider;
    private GameObject _player;
    private InputEventProviderImpl _inputEventProvider;
    

    private IDisposable subscription;
    private bool _canThroughDown = false;
    private float _throughTime = 1f;
    private bool _isCooldown = false;
    private bool _isDown = false;
    void Start()
    {
        _myCollider = GetComponent<BoxCollider2D>();
        _player = GameObject.FindWithTag("Player");
        _inputEventProvider = _player.GetComponent<InputEventProviderImpl>();
        subscription = _inputEventProvider.IsThrough
            .Throttle(TimeSpan.FromSeconds(0.2))
            .Subscribe(x => _canThroughDown = x);

    }

    

    void Update()
    {

        if(_canThroughDown && _isDown)
        {
            _myCollider.enabled = false;
            _isCooldown = true;
            Observable.Timer(TimeSpan.FromSeconds(_throughTime)).Subscribe(_ => EnableCollider());
        }
       
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _isDown = true;
            Debug.Log("‚·‚è”²‚¯‰Â”\");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _isDown = true;
            Debug.Log("‚·‚è”²‚¯•s‰Â”\");
        }
    }

    private void EnableCollider()
    {
        _myCollider.enabled = true;
        _isCooldown = false;
    }



}
