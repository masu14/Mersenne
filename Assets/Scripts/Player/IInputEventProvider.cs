using UniRx;

namespace Merusenne.Player
{
    public interface IInputEventProvider
    {

        IReadOnlyReactiveProperty<float> AxisH { get; }    //…•½•ûŒü‚Ì“ü—Í
        IReadOnlyReactiveProperty<bool> IsJump { get; }
        IReadOnlyReactiveProperty<bool> IsUpSwitch { get; }
        IReadOnlyReactiveProperty<bool> IsDownSwitch { get; }
        IReadOnlyReactiveProperty<bool> IsShot { get; }
    }
}