using Merusenne.Player;
using UnityEngine;
using UniRx;

public class ShotController : MonoBehaviour
{
    private GameObject _player;
    private bool _isWrite = true;
    [SerializeField] private float shotSpeed = 0.02f;   //解除コードの速さ
    [SerializeField] private float shotTime = 1.0f;     //解除コードの消滅までの時間
    private PlayerMove _playerMove;

    private bool _shotxDir;

    private bool ShotxDir
    {
        get { return _shotxDir; }
        set
        {
            if(_isWrite)
            {
                _shotxDir = value;
                _isWrite = false;   //一度書き換えたら以降は書き換え不可
            }
        }
    }

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");                      //プレイヤーオブジェクト取得
        
        _playerMove = _player.GetComponent<PlayerMove>();                //プレイヤーコントローラー取得
        _playerMove.Observable.Subscribe(xDir => ShotxDir = xDir);
    }
   
    private void Update()
    {
        

        
    }

    private void FixedUpdate()
    {
        
        if (_shotxDir == true)                        //スプライトが右向きのとき
        {
            ShotMove(shotSpeed);
        }
        else                                                            //スプライトが左向きのとき
        {
            ShotMove(-shotSpeed);
            Debug.Log("左向きショット");
        }

        Destroy(gameObject, shotTime);                                      //一定時間経過後解除コード消滅
    }

    private void ShotMove(float shotSpeed)                             //解除コードの移動
    {
        transform.Translate(shotSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("接触");
        Destroy(gameObject);
    }
}
