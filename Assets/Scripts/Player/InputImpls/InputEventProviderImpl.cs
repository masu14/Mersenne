using System;
using UniRx;
using UnityEngine;

namespace Merusenne.Player.InputImpls
{
    public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
    {
        private readonly Subject<float> _horizontalSubject = new Subject<float>();
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);

        public IObservable<float> HorizontalObservable => _horizontalSubject;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        void Start()
        {
            _jump.AddTo(this);

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0)
            {
                _horizontalSubject.OnNext(horizontalInput);
            }
        }
        private void Update()
        {
            _jump.Value = Input.GetButton("Jump");
        }

    }
}
    
