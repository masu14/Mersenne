using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class GimmickController : MonoBehaviour
{
    [SerializeField] private Light2D pointLight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.tag == "Shot_blue")
        {
            pointLight.color = new Color32(127, 255, 255, 255);
            ChengeColorOfLight2D(collision, pointLight.color);
        }

      if (collision.gameObject.tag == "Shot_green")
        {
            pointLight.color = new Color32(56, 241, 104, 255);
            ChengeColorOfLight2D(collision, pointLight.color);
        }

      if (collision.gameObject.tag == "Shot_red")
        {
            pointLight.color = new Color32(231, 69, 69, 255);
            ChengeColorOfLight2D(collision, pointLight.color);
        }
        Debug.Log("ÉMÉ~ÉbÉNçÏìÆ");
    }

    private void ChengeColorOfLight2D( Collision2D gameObject, Color32 color)
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
