using System;
using UniRx;
using UnityEngine;

namespace InputEvent
{
    public interface IInputEventProvider
    {

        IObservable<float> HorizontalObservable { get; }    //…•½•ûŒü‚Ì“ü—Í

        IReadOnlyReactiveProperty<bool> IsJump { get; }
    }
}