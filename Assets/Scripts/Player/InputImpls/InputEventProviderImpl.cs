using UniRx;
using UnityEngine;

namespace Merusenne.Player.InputImpls
{
    /// <summary>
    /// ���͂��Ǘ�����N���X,�����Ŏ󂯎�������͂�UniRx��p���đ��N���X�ɑ��M����
    /// </summary>
    
    public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<float> _axisH = new ReactiveProperty<float>(0);           //�̂̌����A���ړ��A(�������L�[����)
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);          //�W�����v�A(Space����)
        private readonly ReactiveProperty<bool> _upSwitch = new ReactiveProperty<bool>(false);      //�V���b�g�؂�ւ��A(����L�[����) 
        private readonly ReactiveProperty<bool> _shot = new ReactiveProperty<bool>(false);          //�V���b�g���ˁA(Ctrl�L�[����)
        private readonly ReactiveProperty<bool> _throughFloor = new ReactiveProperty<bool>(false);  //���蔲�������~���A(�����L�[����)

        //���͂̑��M
        public IReadOnlyReactiveProperty<float> AxisH => _axisH;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<bool> IsUpSwitch => _upSwitch;
        public IReadOnlyReactiveProperty<bool> IsShot => _shot;
        public IReadOnlyReactiveProperty<bool> IsThrough => _throughFloor;
        void Start()
        {
            //OnDestroy����Dispose()�����悤�ɓo�^
            _axisH.AddTo(this);
            _jump.AddTo(this);
            _upSwitch.AddTo(this);
            _shot.AddTo(this);
            _throughFloor.AddTo(this);
        }
        private void Update()
        {
            //���͂̃`�F�b�N
            _axisH.Value = Input.GetAxisRaw("Horizontal");
            _jump.Value = Input.GetButtonDown("Jump");
            _upSwitch.Value = Input.GetKeyDown(KeyCode.UpArrow);
            _shot.Value = Input.GetButtonDown("Fire1");
            _throughFloor.Value = Input.GetKey(KeyCode.DownArrow);
        }

    }
}
    
