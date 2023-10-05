using System.Collections;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

/// <summary>
/// 
/// ロード時のフェードインを行うクラス、Startから徐々にアルファ値を小さくする
/// </summary>
public class FadeController : MonoBehaviour
{
    private Image _fade;
    private GameObject _goal;
    private GoalController _goalController;

    [SerializeField] private  float _fadeInTime = 2.0f;
    [SerializeField] private float _fade_out_time = 2.0f;


    private Color32 _startInColor = new Color32(0, 0, 0, 255);
    private Color32 _startOutColor = new Color32(255, 255, 255, 0);
    private Color32 _endOutColor = new Color32(255, 255, 255, 255);

    private void Awake()
    {
        _goal = GameObject.FindWithTag("Goal");
        _goalController = _goal.GetComponent<GoalController>();

        _goalController.OnGoal
            .Subscribe(_ => FadeOut())
            .AddTo(this);
    }

    void Start()
    {
        

        if (_fade == null)
        {
            _fade = GetComponent<Image>();
        }

        StartCoroutine(FadeIn());


    }

    private IEnumerator FadeIn()
    {
        var timer = 0.0f;
        while (timer < _fadeInTime)
        {
            timer += Time.deltaTime;
            float alpha = 1.0f -timer / _fadeInTime;
            Color newColor = _startInColor;
            newColor.a = alpha;
            _fade.color = newColor;

            yield return null;
        }
    }

    private void FadeOut()
    {
        Debug.Log("FadeOut");
        var timer = 0.0f;
        Color startColor = _startOutColor;
        Color endColor = _endOutColor;
        while(timer<_fade_out_time)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(_startOutColor.a, _endOutColor.a, timer / _fade_out_time);
            Color newColor = _startOutColor;
            newColor.a = alpha;
            _fade.color = newColor;

        }
    }
}
