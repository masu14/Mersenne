using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.VisualScripting;

public class DeadAreaController : MonoBehaviour
{
    private BoxCollider2D _myCollider;
    private BoxCollider2D _parentCollider;

    private BarrierController _barrierController;

    void Start()
    {
        _myCollider = GetComponent<BoxCollider2D>();
        _parentCollider = transform.parent.GetComponent<BoxCollider2D>();

        _barrierController = transform.parent.GetComponent<BarrierController>();

        


        SyncColliderState();


    }

    // Update is called once per frame
    void Update()
    {
        if(_myCollider.enabled != _parentCollider.enabled)
        {
            SyncColliderState();
        }
    }

    void SyncColliderState()
    {
        Debug.Log("SyncColliderState");
        _myCollider.enabled = _parentCollider.enabled;
    }
}
