using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuminaBoardController : MonoBehaviour 
{
    GameObject light2DObject;                       
    Color32 firstLight = new Color32(252, 252, 252, 252);   //白

    private GimmickController grandGimmick;
    
    
    void Start()
    {
        //初期は白に発光
        light2DObject = transform.GetChild(0).gameObject;
        light2DObject.GetComponent<Light2D>().color = firstLight;

        grandGimmick = transform.parent.gameObject.transform.parent.gameObject.GetComponent<GimmickController>();

        if(grandGimmick != null)
        {
            //Debug.Log("祖父オブジェクトの名前：" + grandGimmick.name);
        }
        else
        {
            Debug.Log("祖父オブジェクトはいません"+ gameObject.name);
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
