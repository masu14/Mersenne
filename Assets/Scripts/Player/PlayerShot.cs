using System;
using UnityEngine;
using UniRx;


namespace Merusenne.Player
{
    public class PlayerShot : MonoBehaviour
    {
        private bool _goShot = false;
        private bool _shotxDir = false;
        [SerializeField] private float _goShotTime = 1.5f;
        [SerializeField] private ShotController _shotBluePrefab;
        [SerializeField] private ShotController _shotGreenPrefab;
        [SerializeField] private ShotController _shotRedPrefab;

        private IInputEventProvider _inputEventProvider;

        private Vector3 _shotPoint;
        private ReactiveProperty<int> _shotSwitch = new ReactiveProperty<int>(0);
        public IReactiveProperty<int> OnShotSwitch => _shotSwitch;

        [SerializeField] private PlayerMove _playerMove;

        void Start()
        {
            _inputEventProvider = GetComponent<IInputEventProvider>();

            _shotxDir = true;
            _shotPoint = transform.Find("ShotPoint").localPosition;
            _goShot = true;
            _shotSwitch.AddTo(this);
        }


        void Update()
        {
            if (_inputEventProvider.AxisH.Value > 0.0f)
            {
                _shotxDir = true;
            }
            else if (_inputEventProvider.AxisH.Value < 0.0f)
            {
                _shotxDir = false;

            }


            //ショット切り替え　blue=0, green=1, red=2
            if (_inputEventProvider.IsDownSwitch.Value)
            {
                _shotSwitch.Value++;
                if (_shotSwitch.Value > 2)
                {
                    _shotSwitch.Value = 0;
                }
            }

            if (_inputEventProvider.IsUpSwitch.Value)
            {
                _shotSwitch.Value--;
                if (_shotSwitch.Value < 0)
                {
                    _shotSwitch.Value = 2;
                }
            }

            //ショット発射
            if (Input.GetButtonDown("Fire1") && _goShot == true)
            {
                Shot();
                _goShot = false;
                Observable.Timer(TimeSpan.FromSeconds(_goShotTime)).Subscribe(_ => GoShot());
            }
        }

        void Shot()
        {
            if (_shotSwitch.Value == 0)
            {
                if (_shotxDir)
                {
                    Instantiate(_shotBluePrefab, transform.position + _shotPoint, Quaternion.identity);    //右向き
                }
                else
                {
                    Instantiate(_shotBluePrefab, transform.position + new Vector3(-_shotPoint.x, _shotPoint.y, 0), Quaternion.identity);    //左向き
                }
            }

            if (_shotSwitch.Value == 1)
            {
                if (_shotxDir)
                {
                    Instantiate(_shotGreenPrefab, transform.position + _shotPoint, Quaternion.identity);    //右向き
                }
                else
                {
                    Instantiate(_shotGreenPrefab, transform.position + new Vector3(-_shotPoint.x, _shotPoint.y, 0), Quaternion.identity);    //左向き
                }
            }

            if (_shotSwitch.Value == 2)
            {
                if (_shotxDir)
                {
                    Instantiate(_shotRedPrefab, transform.position + _shotPoint, Quaternion.identity);    //右向き
                }
                else
                {
                    Instantiate(_shotRedPrefab, transform.position + new Vector3(-_shotPoint.x, _shotPoint.y, 0), Quaternion.identity);    //左向き
                }
            }
        }

        void GoShot()
        {
            _goShot = true;
        }
    }
}

