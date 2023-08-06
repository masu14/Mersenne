using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using Merusenne.Player;

public class PlayerShot : MonoBehaviour
{
    private bool _goShot = false;
    private bool _shotxDir = false;
    [SerializeField] private float _goShotTime = 1.5f;

    [SerializeField] private ShotController _shotBluePrefab;
    [SerializeField] private ShotController _shotGreenPrefab;
    [SerializeField] private ShotController _shotRedPrefab;

    private Vector3 _shotPoint;
    private int _shotSwitch = 0;

    [SerializeField] private PlayerMove _playerMove;

    void Start()
    {
        _shotxDir = true;
        _shotPoint = transform.Find("ShotPoint").localPosition;
        _goShot = true;
    }

    
    void Update()
    {
        if (_playerMove.OnAxisH.Value > 0.0f)
        {
            _shotxDir = true;
        }
        else if(_playerMove.OnAxisH.Value < 0.0f)
        {
            _shotxDir = false;
            
        }
        

        //ショット切り替え　blue=0, green=1, red=2
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _shotSwitch++;
            if (_shotSwitch > 2)
            {
                _shotSwitch = 0;
            }
        }

        //ショット発射
        if(Input.GetButtonDown("Fire1") && _goShot == true)
        {
            Shot();
            _goShot = false;
            Observable.Timer(TimeSpan.FromSeconds(_goShotTime)).Subscribe(_ => GoShot());
        }
    }

    void Shot()
    {
        if(_shotSwitch == 0)
        {
            if (_shotxDir)
            {
                Instantiate(_shotBluePrefab, transform.position + _shotPoint, Quaternion.identity);    //右向き
            }
            else
            {
                Instantiate(_shotBluePrefab, transform.position + new Vector3(-_shotPoint.x, _shotPoint.y, 0), Quaternion.identity);    //左向き
            }
        }

        if (_shotSwitch == 1)
        {
            if (_shotxDir)
            {
                Instantiate(_shotGreenPrefab, transform.position + _shotPoint, Quaternion.identity);    //右向き
            }
            else
            {
                Instantiate(_shotGreenPrefab, transform.position + new Vector3(-_shotPoint.x, _shotPoint.y, 0), Quaternion.identity);    //左向き
            }
        }

        if (_shotSwitch == 2)
        {
            if (_shotxDir)
            {
                Instantiate(_shotRedPrefab, transform.position + _shotPoint, Quaternion.identity);    //右向き
            }
            else
            {
                Instantiate(_shotRedPrefab, transform.position + new Vector3(-_shotPoint.x, _shotPoint.y, 0), Quaternion.identity);    //左向き
            }
        }
    }

    void GoShot()
    {
        _goShot = true;
    }
}
