using UniRx;
using UnityEngine;

namespace Merusenne.Player.InputImpls
{
    /// <summary>
    /// �v���C���[�̓��͂��Ǘ�����X�N���v�g�R���|�[�l���g
    /// �����Ŏ󂯎�������͂�UniRx��p���đ��N���X�ɑ��M����
    /// ���͂�IInputEventProvider�C���^�[�t�F�[�X�ɂ�����݂̂̂������A����ȊO�̓G���[�ƂȂ�
    /// </summary>
    
    public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<float> _axisH = new ReactiveProperty<float>(0);           //�̂̌����A���ړ��A(A,D�L�[����)
        private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);          //�W�����v�A(W�L�[����)
        private readonly ReactiveProperty<bool> _leftSwitch = new ReactiveProperty<bool>(false);    //�V���b�g�؂�ւ�(��)�A(���V�t�g�L�[����) 
        private readonly ReactiveProperty<bool> _rightSwitch = new ReactiveProperty<bool>(false);   //�V���b�g�؂�ւ�(�E)�A(�E�V�t�g�L�[����)
        private readonly ReactiveProperty<bool> _shot = new ReactiveProperty<bool>(false);          //�V���b�g���ˁA(Enter�L�[����)
        private readonly ReactiveProperty<bool> _throughFloor = new ReactiveProperty<bool>(false);  //���蔲�������~���A(S�L�[����)

        //���͂̑��M
        public IReadOnlyReactiveProperty<float> AxisH => _axisH;                //PlayerMove,PlayerShot�ɑ��M
        public IReadOnlyReactiveProperty<bool> IsJump => _jump;                 //PlayerMove,PlayerShot�ɑ��M
        public IReadOnlyReactiveProperty<bool> IsLeftSwitch => _leftSwitch;     //PlayerShot�ɑ��M
        public IReadOnlyReactiveProperty<bool> IsRightSwitch => _rightSwitch;   //PlayerShot�ɑ��M
        public IReadOnlyReactiveProperty<bool> IsShot => _shot;                 //PlayerShot�ɑ��M
        public IReadOnlyReactiveProperty<bool> IsThrough => _throughFloor;      //ThroughFloorController�ɑ��M

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
            _axisH.Value = Input.GetAxisRaw("Horizontal");                  //�̂̌����A���ړ��A(A,D�L�[����)
            _jump.Value = Input.GetKeyDown(KeyCode.W);                      //�W�����v�A(W�L�[����)
            _leftSwitch.Value = Input.GetKeyDown(KeyCode.LeftShift);        //�V���b�g�؂�ւ�(��)�A(���V�t�g�L�[����)
            _rightSwitch.Value = Input.GetKeyDown(KeyCode.RightShift);      //�V���b�g�؂�ւ�(�E)�A(�E�V�t�g�L�[����)
            _shot.Value = Input.GetKeyDown(KeyCode.Return);                 //�V���b�g���ˁA(Enter�L�[����)
            _throughFloor.Value = Input.GetKey(KeyCode.S);                  //���蔲�������~���A(S�L�[����)
        }

    }
}
    
