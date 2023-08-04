using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using InputEvent;

public class InputEventProviderImpl : IInputEventProvider
{
    private readonly Subject<float> _horizontalSubject = new Subject<float>();
    private readonly ReactiveProperty<bool> _jump = new ReactiveProperty<bool>(false);

    public IObservable<float> HorizontalObservable => _horizontalSubject;
    public IReadOnlyReactiveProperty<bool> IsJump => _jump;
    void Start()
    {
        
    }

    
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            _horizontalSubject.OnNext(horizontalInput);
        }
    }
}
