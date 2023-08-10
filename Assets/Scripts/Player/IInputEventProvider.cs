using System;
using UniRx;
using UnityEngine;

namespace Merusenne.Player
{
    public interface IInputEventProvider
    {

        IObservable<float> HorizontalObservable { get; }    //水平方向の入力

        IReadOnlyReactiveProperty<bool> IsJump { get; }
        IReadOnlyReactiveProperty<bool> IsUpSwitch { get; }
        IReadOnlyReactiveProperty<bool> IsDownSwitch { get; }
    }
}