using System;
using UniRx;
using UnityEngine;

namespace Merusenne.Player
{
    public interface IInputEventProvider
    {

        IObservable<float> HorizontalObservable { get; }    //���������̓���

        IReadOnlyReactiveProperty<bool> IsJump { get; }
    }
}