using UnityEngine;
using UnityEngine.Rendering.Universal;
using UniRx;
using System;

public class SavePointController : MonoBehaviour
{
    GameObject _light2DObject;
    private Light2D _saveLight;
    [SerializeField] private int _savePointNum;
    private Vector2 _savePointPos;

    //Subject‚ðIObservable‚Æ‚µ‚ÄŒöŠJ‚·‚é
    private Subject<Vector2> _savePointSub = new Subject<Vector2>();
    public IObservable<Vector2> OnTrigger => _savePointSub;
    void Start()
    {
        _light2DObject = transform.GetChild(0).gameObject;
        _saveLight = _light2DObject.GetComponent<Light2D>();
        _savePointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")    //Œã‚Åƒ^ƒO‚É•Ï‚¦‚é
        {
            _saveLight.color = new Color32(252, 252, 252, 252);
            Debug.Log("savepoint");
            _savePointSub.OnNext(_savePointPos);
        }
    }
}
