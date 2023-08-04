using System;
using UniRx;
using UnityEngine;

namespace InputEvent
{
    public interface IInputEventProvider
    {

        IObservable<float> HorizontalObservable { get; }    //���������̓���

        IReadOnlyReactiveProperty<bool> IsJump { get; }
    }
}