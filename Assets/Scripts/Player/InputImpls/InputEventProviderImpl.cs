using System;
using UniRx;
using UnityEngine;

namespace Merusenne.Player.InputImpls
{
    public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<float> _axisH = new ReactiveProperty<float>(0);
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<bool> _upSwitch = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<bool> _downSwitch = new ReactiveProperty<bool>(false);

        public IReadOnlyReactiveProperty<float> AxisH => _axisH;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<bool> IsUpSwitch => _upSwitch;
        public IReadOnlyReactiveProperty<bool> IsDownSwitch => _downSwitch;
        void Start()
        {
            _axisH.AddTo(this);
            _jump.AddTo(this);
            _upSwitch.AddTo(this);
            _downSwitch.AddTo(this);
        }
        private void Update()
        {
            _axisH.Value = Input.GetAxisRaw("Horizontal");
            _jump.Value = Input.GetButton("Jump");
            _upSwitch.Value = Input.GetKeyDown(KeyCode.UpArrow);
            _downSwitch.Value = Input.GetKeyDown(KeyCode.DownArrow);
        }

    }
}
    
