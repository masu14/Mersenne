using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;
using System.Collections.Specialized;

public class GimmickController : MonoBehaviour
{
    [SerializeField] private Light2D pointLight;
    public bool lightBlue = false;
    public bool lightGreen = false;
    public bool lightRed = false;


    private Color shotColor;

    public GameObject luminaBoard;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.tag == "Shot_blue")
        {
            pointLight.color = new Color32(127, 255, 255, 255);
            ChengeColorOfLight2D(collision, pointLight.color);
            lightBlue = true;
        }

      if (collision.gameObject.tag == "Shot_green")
        {
            pointLight.color = new Color32(56, 241, 104, 255);
            ChengeColorOfLight2D(collision, pointLight.color);
            lightGreen = true;
        }

      if (collision.gameObject.tag == "Shot_red")
        {
            pointLight.color = new Color32(231, 69, 69, 255);
            ChengeColorOfLight2D(collision, pointLight.color);
            lightRed = true;
        }
        Debug.Log("ÉMÉ~ÉbÉNçÏìÆ");

    }

    void DoLumina()
    {
        NotifyEvent(luminaBoard);
        Debug.Log("DoLumina");
    }

    void NotifyEvent(GameObject luminaBoard)
    {
        ExecuteEvents.Execute<IEventCaller>(
            target: luminaBoard,
            eventData: null,
            functor: CallMyEvent
            );
        Debug.Log("NotifyEvent");
    }



    void CallMyEvent(IEventCaller inf, BaseEventData eventData)
    {
        inf.EventCall(shotColor);
        Debug.Log("CallMyEvent");
    }

    private void ChengeColorOfLight2D(Collision2D gameObject, Color32 color)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            GameObject light2DObject = this.transform.GetChild(i).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject;
            light2DObject.GetComponent<Light2D>().color = color;
            Debug.Log(i);
        }
        Debug.Log("chengeColor");
    }

}
