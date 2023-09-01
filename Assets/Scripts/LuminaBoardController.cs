using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// LuminaBoard�̔����𐧌䂷��N���X
/// Scene��̓��̊K�w��GimmickObject(�c���I�u�W�F�N�g)���V���b�g�ƏՓ˂���Ɣ�������
/// </summary>
public class LuminaBoardController : MonoBehaviour 
{
    private GameObject _boardLight;                                 //�q�I�u�W�F�N�g��Light
    private GimmickController _grandGimmick;                        //�c���I�u�W�F�N�g��GimmickController

    Color32 _firstLight = new Color32(252, 252, 252, 252);          //���F

    void Start()
    {
        //�����͔��ɔ���
        _boardLight = transform.GetChild(0).gameObject;
        _boardLight.GetComponent<Light2D>().color = _firstLight;

        _grandGimmick = transform.parent.gameObject.transform.parent.gameObject.GetComponent<GimmickController>();  //�c���I�u�W�F�N�g��GimmickController�擾

        //�V���b�g�Փ˂̍w��
        if(_grandGimmick != null)
        {
            _grandGimmick.OnCollision.Subscribe(SetLumina);
        }
    }

    //�V���b�g���Փ˂����炻�̐F�ɔ���
    private void SetLumina(Color32 color)
    {
        _boardLight.GetComponent<Light2D>().color = color;
    }

}
