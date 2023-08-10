using UniRx;
using UnityEngine;

namespace Merusenne.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private PlayerMove _playerMove;
        private PlayerCore _playerCore;
        private IInputEventProvider _inputEventProvider;

        private string _stopAnime = "PlayerStop";
        private string _moveAnime = "PlayerMove";
        private string _jumpAnime = "PlayerJump";
        private string _deadAnime = "PlayerOver";
        private string _nowAnime = "";
        private string _oldAnime = "";

        private void Awake()
        {

            _animator = GetComponent<Animator>();
            _playerMove = GetComponent<PlayerMove>();
            _playerCore = GetComponent<PlayerCore>();
            _inputEventProvider = GetComponent<IInputEventProvider>();

            _playerCore.OnDead.Subscribe(_ => Dead()).AddTo(this);
        }

        private void Start()
        {
            _nowAnime = _stopAnime;
            _oldAnime = _stopAnime;
        }
        void Update()
        {
            //Œü‚«‚Ì’²®
            if (_inputEventProvider.AxisH.Value > 0.0f)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (_inputEventProvider.AxisH.Value < 0.0f)
            {
                transform.localScale = new Vector2(-1, 1);
            }

            if (_playerMove.IsGrounded.Value)
            {
                if (_inputEventProvider.AxisH.Value == 0)
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



        private void Dead()
        {
            _animator.Play(_deadAnime);
        }
    }

}
