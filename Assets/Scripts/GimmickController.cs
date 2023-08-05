using System;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GimmickController : MonoBehaviour
{
    [SerializeField] private Light2D pointLight;   //�q�I�u�W�F�N�g��GimmickLight
    [SerializeField] private GameObject parentBarrier;
        
    private Color32 _blue = new Color32(127, 255, 255, 255);
    private Color32 _green = new Color32(56, 241, 104, 255);
    private Color32 _red = new Color32(231, 69, 69, 255);
    //UniRx��Subject���`
    private Subject<Color32> _collisionColor = new Subject<Color32>();
    private Subject<GameObject> _collisionObject = new Subject<GameObject>();

    

    //LuminaBoard,Barrier���炱��Subject���w�ǂ��邽�߂̃v���p�e�B�����J
    public IObservable<Color32> OnCollision => _collisionColor;
    public IObservable<GameObject> OnCollisionObj => _collisionObject;

    public GameObject luminaBoard;

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //shot�̐F�Ɠ����F�ɔ���
        if (collision.gameObject.tag == "Shot_blue")
        {
            //GimmickObject���ɔ���
            pointLight.color = _blue;
            
            //UniRx����
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
        Debug.Log("�M�~�b�N�쓮");

    }

    
    //luminaBoard�̐F��ς���
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
