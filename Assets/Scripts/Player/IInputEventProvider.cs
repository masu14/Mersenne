using System;
using UniRx;
using UnityEngine;

namespace Merusenne.Player
{
    public interface IInputEventProvider
    {

        IObservable<float> HorizontalObservable { get; }    //…•½•ûŒü‚Ì“ü—Í

        IReadOnlyReactiveProperty<bool> IsJump { get; }
    }
}