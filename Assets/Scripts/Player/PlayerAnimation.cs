using Merusenne.Player;
using UniRx;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rbody;
    private PlayerMove _playerMove;

    private string _stopAnime = "PlayerStop";
    private string _moveAnime = "PlayerMove";
    private string _jumpAnime = "PlayerJump";
    private string _deadAnime = "PlayerOver";
    private string _nowAnime = "";
    private string _oldAnime = "";

    private void Awake()
    {

        _animator = GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody2D>();
        _playerMove = GetComponent<PlayerMove>();


    }

    private void Start()
    {
        _nowAnime = _stopAnime;
        _oldAnime = _stopAnime;
    }
    void Update()
    {
        //Œü‚«‚Ì’²®
        if (_playerMove.OnAxisH.Value > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (_playerMove.OnAxisH.Value < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (_playerMove.IsGrounded.Value)
        {
            if (_playerMove.OnAxisH.Value == 0)
            {
                _nowAnime = _stopAnime;
            }
            else
            {
                _nowAnime = _moveAnime;
            }
        }
        else
        {
            //‹ó’†
            _nowAnime = _jumpAnime;
        }

        if (_nowAnime != _oldAnime)
        {
            _oldAnime = _nowAnime;
            _animator.Play(_nowAnime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _animator.Play(_deadAnime);
    }
}
