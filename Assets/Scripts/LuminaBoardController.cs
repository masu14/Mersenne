using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuminaBoardController : MonoBehaviour ,IEventCaller
{
    GameObject light2DObject;
    Color32 c = new Color32(252, 252, 252, 252);
    Color32 colorOflight;

    

    
    void Start()
    {
        light2DObject = transform.GetChild(0).gameObject;
        light2DObject.GetComponent<Light2D>().color = c;
    }

    

    public void EventCall(Color32 shotColor)
    {
        light2DObject.GetComponent<Light2D>().color = shotColor;
        colorOflight = shotColor;
        Debug.Log("LuminaBoardåƒÇ—èoÇµ");
    }

   

    

}
