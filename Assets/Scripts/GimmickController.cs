using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GimmickController : MonoBehaviour
{
    [SerializeField] private Light2D pointLight;   //子オブジェクトのGimmickLight
    [SerializeField] private GameObject parentBarrier;
    

    private bool _isSwitch;

    public bool lightBlue = false;
    public bool lightGreen = false;
    public bool lightRed = false;

    public bool shotCollision = false;

    public GameObject luminaBoard;

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;     //8:Shotレイヤーに接触したときスイッチを切り替える
        if (layer == 8)
        {
            _isSwitch = !_isSwitch;
        }


        shotCollision = true;
        //shotの色と同じ色に発光
        if (collision.gameObject.tag == "Shot_blue")
        {
            //GimmickObject,LuminaBoardが青に発光
            pointLight.color = new Color32(127, 255, 255, 255);
            ChengeColorOfLight2D(pointLight.color);
            

        }

        if (collision.gameObject.tag == "Shot_green")
        {
            
            pointLight.color = new Color32(56, 241, 104, 255);
            ChengeColorOfLight2D(pointLight.color);
            
            if (lightGreen)
            {
                lightGreen = false;

            }
            else
            {
                lightGreen = true;
            }
        }
        


        if (collision.gameObject.tag == "Shot_red")
        {
            pointLight.color = new Color32(231, 69, 69, 255);
            ChengeColorOfLight2D(pointLight.color);
            
            if(lightRed)
            {
                lightRed = false;
            }
            else
            {
                lightRed = true;
            }
            
        }
        Debug.Log("ギミック作動");

    }

    
    //luminaBoardの色を変える
    private void ChengeColorOfLight2D(Color32 color)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            GameObject light2DObject = this.transform.GetChild(i).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject;
            light2DObject.GetComponent<Light2D>().color = color;
        }
    }

}
