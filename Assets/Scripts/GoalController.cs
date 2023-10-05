using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UniRx;
using System;

public class GoalController : MonoBehaviour
{
    [SerializeField] private Light2D _goal_light;
    [SerializeField] private float _duration = 1.0f;
    [SerializeField] private float _max_intensity = 2.0f;
    [SerializeField] private float _min_intensity = 0.2f;
    private float _time = 0.0f;

    private Subject<Unit> _onGoal = new Subject<Unit>();
    public IObservable<Unit> OnGoal => _onGoal;

    // Start is called before the first frame update
    void Start()
    {
        _onGoal.AddTo(this);   
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        _goal_light.intensity = _min_intensity+_max_intensity*Mathf.Abs( Mathf.Sin(_time * _duration));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _onGoal.OnNext(Unit.Default);
            Debug.Log("Goal");
        }
    }
}
