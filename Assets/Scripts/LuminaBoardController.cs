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

    [SerializeField] private Vector2 size;
    [SerializeField] private LayerMask groundLayer;

    
    void Start()
    {
        light2DObject = transform.GetChild(0).gameObject;
        light2DObject.GetComponent<Light2D>().color = c;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D luminaHit = Physics2D.OverlapBox(transform.position, transform.localScale * 1.1f, 0.0f, groundLayer);
        GameObject luminaLight = luminaHit.transform.GetChild(0).gameObject;

        if (luminaHit.gameObject.tag == "LuminaObject")
        {
            light2DObject.GetComponent<Light2D>().color = luminaLight.GetComponent<Light2D>().color;
        }
    }

    public void ChengeColorOfLight2D(Color32 color)
    {
        light2DObject.GetComponent<Light2D>().color = color;
            Debug.Log("LuminaBoardåƒÇ—èoÇµ");
    }

    public void EventCall(Color32 shotColor)
    {
        light2DObject.GetComponent<Light2D>().color = shotColor;
        colorOflight = shotColor;
        Debug.Log("LuminaBoardåƒÇ—èoÇµ");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LuminaObject")
        {
            light2DObject.GetComponent<Light2D>().color = colorOflight;
        }
    }

    

}
