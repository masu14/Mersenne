using System;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GimmickController : MonoBehaviour
{
    [SerializeField] private Light2D pointLight;   //子オブジェクトのGimmickLight
    [SerializeField] private GameObject parentBarrier;
        
    private Color32 _blue = new Color32(127, 255, 255, 255);
    private Color32 _green = new Color32(56, 241, 104, 255);
    private Color32 _red = new Color32(231, 69, 69, 255);
    //UniRxのSubjectを定義
    private Subject<Color32> _collisionColor = new Subject<Color32>();
    private Subject<GameObject> _collisionObject = new Subject<GameObject>();

    

    //LuminaBoard,BarrierからこのSubjectを購読するためのプロパティを公開
    public IObservable<Color32> OnCollision => _collisionColor;
    public IObservable<GameObject> OnCollisionObj => _collisionObject;

    public GameObject luminaBoard;

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //shotの色と同じ色に発光
        if (collision.gameObject.tag == "Shot_blue")
        {
            //GimmickObjectが青に発光
            pointLight.color = _blue;
            
            //UniRx処理
            _collisionColor.OnNext(_blue);
            _collisionObject.OnNext(collision.gameObject);

        }

        if (collision.gameObject.tag == "Shot_green")
        {

            pointLight.color = _green;
            _collisionColor.OnNext(_green);
            _collisionObject.OnNext(collision.gameObject);
        }
        


        if (collision.gameObject.tag == "Shot_red")
        {
            pointLight.color = _red;
            _collisionColor.OnNext(_red);
            _collisionObject.OnNext(collision.gameObject);
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

    bool InvertBool(bool value)
    {
        return !value;
    }
}
