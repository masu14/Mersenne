using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuminaBoardController : MonoBehaviour 
{
    GameObject light2DObject;                       
    Color32 firstLight = new Color32(252, 252, 252, 252);   //��

    private GimmickController grandGimmick;
    
    
    void Start()
    {
        //�����͔��ɔ���
        light2DObject = transform.GetChild(0).gameObject;
        light2DObject.GetComponent<Light2D>().color = firstLight;

        grandGimmick = transform.parent.gameObject.transform.parent.gameObject.GetComponent<GimmickController>();

        if(grandGimmick != null)
        {
            //Debug.Log("�c���I�u�W�F�N�g�̖��O�F" + grandGimmick.name);
        }
        else
        {
            Debug.Log("�c���I�u�W�F�N�g�͂��܂���"+ gameObject.name);
        }

        if(grandGimmick != null)
        {
            grandGimmick.OnCollision.Subscribe(SetLumina);
        }
    }

    private void SetLumina(Color32 color)
    {
        light2DObject.GetComponent<Light2D>().color = color;
    }

}
