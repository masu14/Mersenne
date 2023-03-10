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

    public bool shotCollision = false;

    


    private Color shotColor;

    public GameObject luminaBoard;

    private void Start()
    {
     
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        shotCollision = true;
        //shotの色と同じ色に発光
        if (collision.gameObject.tag == "Shot_blue")
        {
            
            pointLight.color = new Color32(127, 255, 255, 255);
            ChengeColorOfLight2D(collision, pointLight.color);
            
            if(lightBlue)
            {
                lightBlue = false;

            }
            else
            {
                lightBlue = true;
            }
        }

        if (collision.gameObject.tag == "Shot_green")
        {
            
            pointLight.color = new Color32(56, 241, 104, 255);
            ChengeColorOfLight2D(collision, pointLight.color);
            
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
            shotCollision = true;
            pointLight.color = new Color32(231, 69, 69, 255);
            ChengeColorOfLight2D(collision, pointLight.color);
            
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
    private void ChengeColorOfLight2D(Collision2D gameObject, Color32 color)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            GameObject light2DObject = this.transform.GetChild(i).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject;
            light2DObject.GetComponent<Light2D>().color = color;
        }
    }

}
