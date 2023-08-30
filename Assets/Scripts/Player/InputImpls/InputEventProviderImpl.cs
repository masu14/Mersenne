using UniRx;
using UnityEngine;

namespace Merusenne.Player.InputImpls
{
    //入力を管理するクラス,ここで受け取った入力をUniRxを用いて他クラスに送信する
    public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<float> _axisH = new ReactiveProperty<float>(0);           //体の向き、横移動、(水平矢印キー入力)
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);          //ジャンプ、(Space入力)
        private readonly ReactiveProperty<bool> _upSwitch = new ReactiveProperty<bool>(false);      //ショット切り替え、(上矢印キー入力) 
        private readonly ReactiveProperty<bool> _shot = new ReactiveProperty<bool>(false);          //ショット発射、(Ctrlキー入力)
        private readonly ReactiveProperty<bool> _throughFloor = new ReactiveProperty<bool>(false);  //すり抜け床を降りる、(下矢印キー入力)

        public IReadOnlyReactiveProperty<float> AxisH => _axisH;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<bool> IsUpSwitch => _upSwitch;
        public IReadOnlyReactiveProperty<bool> IsShot => _shot;
        public IReadOnlyReactiveProperty<bool> IsThrough => _throughFloor;
        void Start()
        {
            //入力の送信
            _axisH.AddTo(this);
            _jump.AddTo(this);
            _upSwitch.AddTo(this);
            _shot.AddTo(this);
            _throughFloor.AddTo(this);
        }
        private void Update()
        {
            //入力のチェック
            _axisH.Value = Input.GetAxisRaw("Horizontal");
            _jump.Value = Input.GetButtonDown("Jump");
            _upSwitch.Value = Input.GetKeyDown(KeyCode.UpArrow);
            _shot.Value = Input.GetButtonDown("Fire1");
            _throughFloor.Value = Input.GetKey(KeyCode.DownArrow);
        }

    }
}
    
