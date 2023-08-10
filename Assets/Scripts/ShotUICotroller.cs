using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Merusenne.Player;

public class ShotUICotroller : MonoBehaviour
{
    private GameObject _player;
    private PlayerShot _playerShot;

    private int _shotColor =0;
    private Color32 _onColor = new Color32(255, 255, 255, 255);
    private Color32 _offColor = new Color32(255, 255, 255, 100);
    void Start()
    {
        
        _player = GameObject.FindWithTag("Player");
        _playerShot = _player.GetComponent<PlayerShot>();
        _playerShot.OnShotSwitch.Subscribe(x =>
        {
            _shotColor = x;
            UpdateColor();
            });
        
        
    }


    private void UpdateColor()
    {
        switch (_shotColor)
        {
            case 0:
                if (gameObject.tag == "Blue_shot_UI")
                {
                    
                    GetComponent<Image>().color = _onColor;
                    Debug.Log("青ショットUI");
                    Debug.Log(_onColor.a);
                }
                else
                {
                    GetComponent<Image>().color = _offColor;
                }
                break;
            case 1:
                if (gameObject.tag == "Green_shot_UI")
                {
                    GetComponent<Image>().color = _onColor;
                    Debug.Log("緑ショットUI");
                    Debug.Log(_onColor.a);
                }
                else
                {
                    GetComponent<Image>().color = _offColor;
                }
                break;
            case 2:
                if (gameObject.tag == "Red_shot_UI")
                {
                    GetComponent<Image>().color = _onColor;
                    Debug.Log("赤ショットUI");
                    Debug.Log(_onColor.a);
                }
                else
                {
                    GetComponent<Image>().color = _offColor;
                }
                break;
            
        }
    }
}
