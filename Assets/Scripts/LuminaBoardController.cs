using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuminaBoardController : MonoBehaviour 
{
    GameObject light2DObject;                       
    Color32 firstLight = new Color32(252, 252, 252, 252);    

    
    void Start()
    {
        light2DObject = transform.GetChild(0).gameObject;
        light2DObject.GetComponent<Light2D>().color = firstLight;
    }

}
