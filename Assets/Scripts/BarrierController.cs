using UnityEngine;
using UnityEngine.Rendering.Universal;
using UniRx;

public class BarrierController : MonoBehaviour
{
    private Light2D _barrierLight;                              //子オブジェクトのLight

    private GimmickController grandGimmick;                     //祖父オブジェクトのGimmickController
    [SerializeField] private bool _barrierActive = false;       //Barrier明滅フラグ

    private Color32 _blue = new Color32(127, 255, 255, 255);    //青
    private Color32 _green = new Color32(56, 241, 104, 255);    //緑
    private Color32 _red = new Color32(231, 69, 69, 255);       //赤

    BoxCollider2D boxCollider2D;                                //アクティブ状態の管理に使用

    private void Start()
    {
        _barrierLight = transform.GetChild(0).gameObject.GetComponent<Light2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        grandGimmick = transform.parent.gameObject.transform.parent.gameObject.GetComponent<GimmickController>();

        if (grandGimmick != null)
        {
            Debug.Log("祖父オブジェクトの名前：" + grandGimmick.name);
        }
        else
        {
            Debug.Log("祖父オブジェクトはいません" + gameObject.name);
        }

        if (grandGimmick != null)
        {
            grandGimmick.OnCollisionObj.Subscribe(SwitchBarrierLight);      //ショット衝突の受信
        }

        //Barrierの初期化
        if (_barrierActive == false)
        {
            boxCollider2D.enabled = false;
            _barrierLight.color = new Color32(0, 0, 0, 0);
        }
    }

    private void OnDestroy()
    {
        grandGimmick.OnCollisionObj.Subscribe(SwitchBarrierLight).Dispose();
    }

    private void SwitchBarrierLight(GameObject gameObject)
    {
        //青Barrierの発光、消滅
        if (gameObject.tag == "Shot_blue")
        {
            if(this.gameObject.tag == "Barrier_blue")
            {
                if (_barrierActive) //消滅
                {
                    boxCollider2D.enabled = false;
                    _barrierLight.color = new Color32(0, 0, 0, 0);
                    _barrierActive = !_barrierActive;
                }
                else　              //発光
                {
                    boxCollider2D.enabled = true;
                    _barrierLight.color = _blue;
                    _barrierActive = !_barrierActive;
                }
            }
            
        }

        //緑Barrierの発光、消滅
        if (gameObject.tag == "Shot_green")
        {
            if (this.gameObject.tag == "Barrier_green")
            {
                if (_barrierActive)
                {
                    boxCollider2D.enabled = false;
                    _barrierLight.color = new Color32(0, 0, 0, 0);
                    _barrierActive = !_barrierActive;
                }
                else
                {
                    boxCollider2D.enabled = true;
                    _barrierLight.color = _green;
                    _barrierActive = !_barrierActive;
                }
            }

        }
        //赤Barrierの発光、消滅
        if (gameObject.tag == "Shot_red")
        {
            if (this.gameObject.tag == "Barrier_red")
            {
                
                if (_barrierActive) //消滅
                {
                    boxCollider2D.enabled = false;
                    _barrierLight.color = new Color32(0, 0, 0, 0);
                    _barrierActive = !_barrierActive;
                }
                else　             //発光
                {
                    boxCollider2D.enabled = true;
                    _barrierLight.color = _red;
                    _barrierActive = !_barrierActive;
                }
            }

        }
    }
    

    
}
