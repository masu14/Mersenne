using System;
using UnityEngine;
using UniRx;


namespace Merusenne.Player
{
    /// <summary>
    /// プレイヤーのショットの発射の制御を行うクラス
    /// ショット本体の制御はShotControllerが行う
    /// </summary>
    public class PlayerShot : MonoBehaviour
    {
        private IInputEventProvider _inputEventProvider;

        //パラメータ
        [SerializeField] private float _goShotTime = 1.5f;                          //ショット待機時間

        //Prefabの登録
        [SerializeField] private ShotController _shotBluePrefab;                    //青ショット
        [SerializeField] private ShotController _shotGreenPrefab;                   //緑ショット
        [SerializeField] private ShotController _shotRedPrefab;                     //赤ショット

                    
        private Vector3 _shotPoint;                                                 //ショットが生成される位置
        private bool _goShot = false;                                               //ショットフラグ
        private bool _shotxDir = false;                                             //ショット発射向き(true:右向き、false:左向き)
        private const float _AXISHBORDER = 0.15f;                                   //横入力の閾値

        private ReactiveProperty<int> _shotSwitch = new ReactiveProperty<int>(0);   //ショットの切り替えを保持

        //ショットの切り替えを送信
        public IReactiveProperty<int> OnShotSwitch => _shotSwitch;


        void Start()
        {
            _inputEventProvider = GetComponent<IInputEventProvider>();      //プレイヤーの入力取得
            _shotPoint = transform.Find("ShotPoint").localPosition;         //ショットの生成される位置を取得

            //初期化
            _shotxDir = true;
            _goShot = true;

            //OnDestroy時にDispose()されるように登録
            _shotSwitch.AddTo(this);
        }


        void Update()
        {
            if (_inputEventProvider.AxisH.Value > _AXISHBORDER)
            {
                _shotxDir = true;
            }
            else if (_inputEventProvider.AxisH.Value < -_AXISHBORDER)
            {
                _shotxDir = false;

            }


            //ショット切り替え　blue=0, green=1, red=2
            if (_inputEventProvider.IsUpSwitch.Value)
            {
                _shotSwitch.Value--;
                if (_shotSwitch.Value < 0)
                {
                    _shotSwitch.Value = 2;
                }
            }

            //ショット発射
            if (_inputEventProvider.IsShot.Value && _goShot == true)
            {
                Shot();
                _goShot = false;
                Observable.Timer(TimeSpan.FromSeconds(_goShotTime)).Subscribe(_ => GoShot());
            }
        }

        //ショットの生成
        void Shot()
        {
            //ショットの色と体の向きで場合分け
            switch (_shotSwitch.Value)
            {
                case 0: //青ショット
                    if (_shotxDir)
                    {
                        Instantiate(_shotBluePrefab, transform.position + _shotPoint, Quaternion.identity);    //右向き
                    }
                    else
                    {
                        Instantiate(_shotBluePrefab, transform.position + new Vector3(-_shotPoint.x, _shotPoint.y, 0), Quaternion.identity);    //左向き
                    }
                    break;
                case 1: //緑ショット
                    if (_shotxDir)
                    {
                        Instantiate(_shotGreenPrefab, transform.position + _shotPoint, Quaternion.identity);    //右向き
                    }
                    else
                    {
                        Instantiate(_shotGreenPrefab, transform.position + new Vector3(-_shotPoint.x, _shotPoint.y, 0), Quaternion.identity);    //左向き
                    }
                    break;
                case 2: //赤ショット
                    if (_shotxDir)
                    {
                        Instantiate(_shotRedPrefab, transform.position + _shotPoint, Quaternion.identity);    //右向き
                    }
                    else
                    {
                        Instantiate(_shotRedPrefab, transform.position + new Vector3(-_shotPoint.x, _shotPoint.y, 0), Quaternion.identity);    //左向き
                    }
                    break;
            }
            
        }

        //待機時間が経過したらショットの発射が可能になる
        void GoShot()
        {
            _goShot = true;
        }
    }
}

