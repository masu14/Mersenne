using UniRx;
using UnityEngine;

namespace Merusenne.Player.InputImpls
{
    /// <summary>
    /// ���͂��Ǘ�����N���X,�����Ŏ󂯎�������͂�UniRx��p���đ��N���X�ɑ��M����
    /// </summary>
    
    public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<float> _axisH = new ReactiveProperty<float>(0);           //�̂̌����A���ړ��A(A,D�L�[����)
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);          //�W�����v�A(Space����)
        private readonly ReactiveProperty<bool> _leftSwitch = new ReactiveProperty<bool>(false);    //�V���b�g�؂�ւ�(��)�A(���V�t�g�L�[����) 
        private readonly ReactiveProperty<bool> _rightSwitch = new ReactiveProperty<bool>(false);   //�V���b�g�؂�ւ�(�E)�A(�E�V�t�g�L�[����)
        private readonly ReactiveProperty<bool> _shot = new ReactiveProperty<bool>(false);          //�V���b�g���ˁA(Enter�L�[����)
        private readonly ReactiveProperty<bool> _throughFloor = new ReactiveProperty<bool>(false);  //���蔲�������~���A(S�L�[����)

        //���͂̑��M
        public IReadOnlyReactiveProperty<float> AxisH => _axisH;
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;
        public IReadOnlyReactiveProperty<bool> IsLeftSwitch => _leftSwitch;
        public IReadOnlyReactiveProperty<bool> IsRightSwitch => _rightSwitch;
        public IReadOnlyReactiveProperty<bool> IsShot => _shot;
        public IReadOnlyReactiveProperty<bool> IsThrough => _throughFloor;
        void Start()
        {
            //OnDestroy����Dispose()�����悤�ɓo�^
            _axisH.AddTo(this);
            _jump.AddTo(this);
            _leftSwitch.AddTo(this);
            _rightSwitch.AddTo(this);
            _shot.AddTo(this);
            _throughFloor.AddTo(this);
        }
        private void Update()
        {
            //���͂̃`�F�b�N
            _axisH.Value = Input.GetAxisRaw("Horizontal");
            _jump.Value = Input.GetKeyDown(KeyCode.W);
            _leftSwitch.Value = Input.GetKeyDown(KeyCode.LeftShift);
            _rightSwitch.Value = Input.GetKeyDown(KeyCode.RightShift);
            _shot.Value = Input.GetKeyDown(KeyCode.Return);
            _throughFloor.Value = Input.GetKey(KeyCode.S);
        }

    }
}
    
