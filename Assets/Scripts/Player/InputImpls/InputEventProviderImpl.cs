using System;
using UniRx;
using UnityEngine;

namespace Merusenne.Player.InputImpls
{
    public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
    {
        private readonly Subject<float> _horizontalSubject = new Subject<float>();
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<bool> _upSwitch = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<bool> _downSwitch = new ReactiveProperty<bool>(false);

        public IObservable<float> HorizontalObservable => _horizontalSubject;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<bool> IsUpSwitch => _upSwitch;
        public IReadOnlyReactiveProperty<bool> IsDownSwitch => _downSwitch;
        void Start()
        {
            _jump.AddTo(this);

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0)
            {
                _horizontalSubject.OnNext(horizontalInput);
            }

            _upSwitch.AddTo(this);
            _downSwitch.AddTo(this);
        }
        private void Update()
        {
            _jump.Value = Input.GetButton("Jump");
            _upSwitch.Value = Input.GetKeyDown(KeyCode.UpArrow);
            _downSwitch.Value = Input.GetKeyDown(KeyCode.DownArrow);
        }

    }
}
    
