using UniRx;
using UnityEngine;

namespace Merusenne.Player.InputImpls
{
    /// <summary>
    /// 入力を管理するクラス,ここで受け取った入力をUniRxを用いて他クラスに送信する
    /// </summary>
    
    public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<float> _axisH = new ReactiveProperty<float>(0);           //体の向き、横移動、(A,Dキー入力)
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);          //ジャンプ、(Space入力)
        private readonly ReactiveProperty<bool> _leftSwitch = new ReactiveProperty<bool>(false);    //ショット切り替え(左)、(左シフトキー入力) 
        private readonly ReactiveProperty<bool> _rightSwitch = new ReactiveProperty<bool>(false);   //ショット切り替え(右)、(右シフトキー入力)
        private readonly ReactiveProperty<bool> _shot = new ReactiveProperty<bool>(false);          //ショット発射、(Enterキー入力)
        private readonly ReactiveProperty<bool> _throughFloor = new ReactiveProperty<bool>(false);  //すり抜け床を降りる、(Sキー入力)

        //入力の送信
        public IReadOnlyReactiveProperty<float> AxisH => _axisH;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<bool> IsLeftSwitch => _leftSwitch;
        public IReadOnlyReactiveProperty<bool> IsRightSwitch => _rightSwitch;
        public IReadOnlyReactiveProperty<bool> IsShot => _shot;
        public IReadOnlyReactiveProperty<bool> IsThrough => _throughFloor;
        void Start()
        {
            //OnDestroy時にDispose()されるように登録
            _axisH.AddTo(this);
            _jump.AddTo(this);
            _leftSwitch.AddTo(this);
            _rightSwitch.AddTo(this);
            _shot.AddTo(this);
            _throughFloor.AddTo(this);
        }
        private void Update()
        {
            //入力のチェック
            _axisH.Value = Input.GetAxisRaw("Horizontal");
            _jump.Value = Input.GetKeyDown(KeyCode.W);
            _leftSwitch.Value = Input.GetKeyDown(KeyCode.LeftShift);
            _rightSwitch.Value = Input.GetKeyDown(KeyCode.RightShift);
            _shot.Value = Input.GetKeyDown(KeyCode.Return);
            _throughFloor.Value = Input.GetKey(KeyCode.S);
        }

    }
}
    
