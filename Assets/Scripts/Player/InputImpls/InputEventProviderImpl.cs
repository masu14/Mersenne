using UniRx;
using UnityEngine;

namespace Merusenne.Player.InputImpls
{
    //入力を管理するクラス
    public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<float> _axisH = new ReactiveProperty<float>(0);           //水平入力
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);          //ジャンプ入力
        private readonly ReactiveProperty<bool> _upSwitch = new ReactiveProperty<bool>(false);      //ショット切り替え上矢印キー入力
        private readonly ReactiveProperty<bool> _downSwitch = new ReactiveProperty<bool>(false);    //ショット切り替え下矢印キー入力
        private readonly ReactiveProperty<bool> _shot = new ReactiveProperty<bool>(false);
        private readonly ReactiveProperty<bool> _throughFloor = new ReactiveProperty<bool>(false);

        public IReadOnlyReactiveProperty<float> AxisH => _axisH;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<bool> IsUpSwitch => _upSwitch;
        public IReadOnlyReactiveProperty<bool> IsDownSwitch => _downSwitch;
        public IReadOnlyReactiveProperty<bool> IsShot => _shot;
        public IReadOnlyReactiveProperty<bool> IsThrough => _throughFloor;
        void Start()
        {
            _axisH.AddTo(this);
            _jump.AddTo(this);
            _upSwitch.AddTo(this);
            _downSwitch.AddTo(this);
            _shot.AddTo(this);
            _throughFloor.AddTo(this);
        }
        private void Update()
        {
            _axisH.Value = Input.GetAxisRaw("Horizontal");
            _jump.Value = Input.GetButtonDown("Jump");
            _upSwitch.Value = Input.GetKeyDown(KeyCode.UpArrow);
            _downSwitch.Value = Input.GetKeyDown(KeyCode.Tab);
            _shot.Value = Input.GetButtonDown("Fire1");
            _throughFloor.Value = Input.GetKey(KeyCode.DownArrow);
        }

    }
}
    
