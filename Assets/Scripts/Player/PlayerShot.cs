using System;
using UnityEngine;
using UniRx;


namespace Merusenne.Player
{
    /// <summary>
    /// �v���C���[�̃V���b�g�̔��˂̐�����s���N���X
    /// �V���b�g�{�̂̐����ShotController���s��
    /// </summary>
    public class PlayerShot : MonoBehaviour
    {
        private IInputEventProvider _inputEventProvider;

        //�p�����[�^
        [SerializeField] private float _goShotTime = 1.5f;                          //�V���b�g�ҋ@����

        //Prefab�̓o�^
        [SerializeField] private ShotController _shotBluePrefab;                    //�V���b�g
        [SerializeField] private ShotController _shotGreenPrefab;                   //�΃V���b�g
        [SerializeField] private ShotController _shotRedPrefab;                     //�ԃV���b�g
       
        private Vector3 _shotPoint;                                                 //�V���b�g�����������ʒu
        private bool _goShot = false;                                               //�V���b�g�t���O
        private bool _shotxDir = false;                                             //�V���b�g���ˌ���(true:�E�����Afalse:������)
        private const float _AXISHBORDER = 0.15f;                                   //�����͂�臒l

        private ReactiveProperty<int> _shotSwitch = new ReactiveProperty<int>(0);   //�V���b�g�̐؂�ւ���ێ�

        //�V���b�g�̐؂�ւ��𑗐M
        public IReactiveProperty<int> OnShotSwitch => _shotSwitch;

        void Start()
        {
            _inputEventProvider = GetComponent<IInputEventProvider>();      //�v���C���[�̓��͎擾
            _shotPoint = transform.Find("ShotPoint").localPosition;         //�V���b�g�̐��������ʒu���擾

            //������
            _shotxDir = true;
            _goShot = true;

            //OnDestroy����Dispose()�����悤�ɓo�^
            _shotSwitch.AddTo(this);
        }


        void Update()
        {
            //���͂��󂯎���ăV���b�g�����̌��������߂�
            if (_inputEventProvider.AxisH.Value > _AXISHBORDER)         //�E����
            {
                _shotxDir = true;
            }
            else if (_inputEventProvider.AxisH.Value < -_AXISHBORDER)   //������
            {
                _shotxDir = false;

            }


            //�V���b�g�؂�ւ��@blue=0, green=1, red=2
            if (_inputEventProvider.IsUpSwitch.Value)
            {
                //0=>1=>2=>0�̏��ԂŐ؂�ւ��
                _shotSwitch.Value--;
                if (_shotSwitch.Value < 0)
                {
                    _shotSwitch.Value = 2;
                }
            }

            //�V���b�g����
            if (_inputEventProvider.IsShot.Value && _goShot == true)
            {
                Shot();
                _goShot = false;
                Observable.Timer(TimeSpan.FromSeconds(_goShotTime)).Subscribe(_ => GoShot());
            }
        }

        //�V���b�g�̐���
        void Shot()
        {
            ShotController shotPrefab = null;

            //�F�ԍ����琶������V���b�g�����߂�
            switch (_shotSwitch.Value)
            {
                case 0: //�V���b�g
                    shotPrefab = _shotBluePrefab;
                    break;
                case 1: //�΃V���b�g
                    shotPrefab = _shotGreenPrefab;
                    break;
                case 2: //�ԃV���b�g
                    shotPrefab = _shotRedPrefab;
                    break;
            }

            //�̂̌�������V���b�g�����������ʒu�����߂�
            Vector3 shotPosition = transform.position + (_shotxDir ? _shotPoint : new Vector3(-_shotPoint.x, _shotPoint.y, 0));

            //�V���b�g����
            Instantiate(shotPrefab, shotPosition, Quaternion.identity);
          
        }

        //�ҋ@���Ԃ��o�߂�����V���b�g�̔��˂��\�ɂȂ�
        void GoShot()
        {
            _goShot = true;
        }
    }
}

