using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// ロード時のフェードインを行うクラス、Startから徐々にアルファ値を小さくする
/// </summary>
public class FadeController : MonoBehaviour
{

    private SpriteRenderer _fade;
    private float _fadeInTime = 1.0f;

    private Color32 _startColor = new Color32(0, 0, 0, 255);
    private float _timer = 0.0f;

    void Start()
    {
        if(_fade ==null)
        {
            _fade = GetComponent<SpriteRenderer>();
        }

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (_timer < _fadeInTime)
        {
            _timer += Time.deltaTime;
            float alpha = 1.0f -_timer / _fadeInTime;
            Color newColor = _startColor;
            newColor.a = alpha;
            _fade.color = newColor;

            yield return null;
        }

        
    }
}
