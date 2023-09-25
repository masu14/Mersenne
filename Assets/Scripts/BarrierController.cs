using UnityEngine;
using UnityEngine.Rendering.Universal;
using UniRx;

/// <summary>
/// Barrier�𐧌䂷��N���X
/// Barrier�͐e�I�u�W�F�N�g��GimmickObject�ɃV���b�g���Փ˂����Ƃ����̐F�ɉ����Ė��ł���
/// light���_���Ă���Ƃ��R���C�_�[���A�N�e�B�u�A�����Ă���Ƃ���A�N�e�B�u��ԂɂȂ�
/// </summary>
public class BarrierController : MonoBehaviour
{
    private Light2D _barrierLight;                              //�q�I�u�W�F�N�g��Light
    private BoxCollider2D _boxCollider2D;                        //�A�N�e�B�u��Ԃ̊Ǘ��Ɏg�p
    private GimmickController _grandGimmick;                     //�c���I�u�W�F�N�g��GimmickController

    //�p�����[�^
    [SerializeField] private bool _barrierActive = false;       //Barrier���Ńt���O

    //light2D�̐F
    private Color32 _blue = new Color32(127, 255, 255, 255);    //�F
    private Color32 _green = new Color32(56, 241, 104, 255);    //�ΐF
    private Color32 _red = new Color32(231, 69, 69, 255);       //�ԐF
    private Color32 _clear = Color.clear;                       //���F

    private readonly ReactiveProperty<bool> _barrierState = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> OnBarrier => _barrierState; 
   

    private void Start()
    {
        _barrierLight = transform.GetChild(0).gameObject.GetComponent<Light2D>();                                   //Barrier��light�擾
        _boxCollider2D = transform.GetComponent<BoxCollider2D>();                                                    //Barrier�̃R���C�_�[�擾
        _grandGimmick = transform.parent.gameObject.transform.parent.gameObject.GetComponent<GimmickController>();   //�c����Gimmick�擾

        
        if (_grandGimmick != null)
        {
            //�V���b�g�Փ˂̍w��
            _grandGimmick.OnCollisionObj.Subscribe(SwitchBarrierLight);      
        }

        //Barrier�̏�����
        if (_barrierActive == false)
        {
            _boxCollider2D.enabled = false;
            _barrierState.Value = false;
            _barrierLight.color = _clear;
        }

        _barrierState.AddTo(this);
    }

    
    //�v���C���[�̃V���b�g�Փˎ��ɌĂяo���ABarrier�Ɠ����F�̃V���b�g���Փ˂���ƐF�ƃR���C�_�[���ω�����
    private void SwitchBarrierLight(GameObject gameObject)
    {
        //�V���b�g�̃^�O�m�F
        if (gameObject.CompareTag("Shot_blue") || gameObject.CompareTag("Shot_green") || gameObject.CompareTag("Shot_red"))
        {
            string barrierTag = $"Barrier_{gameObject.tag.Substring(5)}";   //"Shot_blue" => "Barrier_blue"

            //�V���b�g�̎��g�̃^�O����Ή������F�ƃR���C�_�[��؂�ւ���
            if (this.gameObject.CompareTag(barrierTag))
            {
                _barrierActive = !_barrierActive;
                _boxCollider2D.enabled = _barrierActive;
                _barrierState.Value = _barrierActive;
                _barrierLight.color = _barrierActive ? GetBarrierColor(gameObject.tag) : _clear;

            }
        }
    }
    //�Փ˂����V���b�g�̃^�O����Ή������F��Ԃ�
    private Color32 GetBarrierColor(string shotTag)
    {
        switch (shotTag)
        {
            case "Shot_blue":   //�F
                return _blue;   
            case "Shot_green":  //�ΐF
                return _green;
            case "Shot_red":    //�ԐF
                return _red;
            default:            //���F
                return _clear;
        }
    }

    
}
