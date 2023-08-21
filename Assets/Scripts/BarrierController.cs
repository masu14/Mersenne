using UnityEngine;
using UnityEngine.Rendering.Universal;
using UniRx;

public class BarrierController : MonoBehaviour
{
    private Light2D _barrierLight;                              //�q�I�u�W�F�N�g��Light

    private GimmickController grandGimmick;                     //�c���I�u�W�F�N�g��GimmickController
    [SerializeField] private bool _barrierActive = false;       //Barrier���Ńt���O

    private Color32 _blue = new Color32(127, 255, 255, 255);    //��
    private Color32 _green = new Color32(56, 241, 104, 255);    //��
    private Color32 _red = new Color32(231, 69, 69, 255);       //��

    BoxCollider2D boxCollider2D;                                //�A�N�e�B�u��Ԃ̊Ǘ��Ɏg�p

    private void Start()
    {
        _barrierLight = transform.GetChild(0).gameObject.GetComponent<Light2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        grandGimmick = transform.parent.gameObject.transform.parent.gameObject.GetComponent<GimmickController>();

        if (grandGimmick != null)
        {
            Debug.Log("�c���I�u�W�F�N�g�̖��O�F" + grandGimmick.name);
        }
        else
        {
            Debug.Log("�c���I�u�W�F�N�g�͂��܂���" + gameObject.name);
        }

        if (grandGimmick != null)
        {
            grandGimmick.OnCollisionObj.Subscribe(SwitchBarrierLight);      //�V���b�g�Փ˂̎�M
        }

        //Barrier�̏�����
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
        //��Barrier�̔����A����
        if (gameObject.tag == "Shot_blue")
        {
            if(this.gameObject.tag == "Barrier_blue")
            {
                if (_barrierActive) //����
                {
                    boxCollider2D.enabled = false;
                    _barrierLight.color = new Color32(0, 0, 0, 0);
                    _barrierActive = !_barrierActive;
                }
                else�@              //����
                {
                    boxCollider2D.enabled = true;
                    _barrierLight.color = _blue;
                    _barrierActive = !_barrierActive;
                }
            }
            
        }

        //��Barrier�̔����A����
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
        //��Barrier�̔����A����
        if (gameObject.tag == "Shot_red")
        {
            if (this.gameObject.tag == "Barrier_red")
            {
                
                if (_barrierActive) //����
                {
                    boxCollider2D.enabled = false;
                    _barrierLight.color = new Color32(0, 0, 0, 0);
                    _barrierActive = !_barrierActive;
                }
                else�@             //����
                {
                    boxCollider2D.enabled = true;
                    _barrierLight.color = _red;
                    _barrierActive = !_barrierActive;
                }
            }

        }
    }
    

    
}
