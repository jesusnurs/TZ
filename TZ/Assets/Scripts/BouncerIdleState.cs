using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class BouncerIdleState : IState
{
    private readonly Animator _animator;
    private readonly MonoBehaviour _runner;
    private readonly List<string> _idleAnimations;
    private readonly Vector2 _intervalRange;
    private readonly Action _onRandomAction;

    private Coroutine _timer;
    
    public BouncerIdleState(Animator animator, MonoBehaviour runner, List<string> idleAnimations, Vector2 intervalRange, Action onRandomAction)
    {
        _animator = animator;
        _runner = runner;
        _idleAnimations = idleAnimations;
        _intervalRange = intervalRange;
        _onRandomAction = onRandomAction;
    }

    public void Enter()
    {
        _animator.CrossFade(_idleAnimations[Random.Range(0, _idleAnimations.Count)], 0f);
        _timer = _runner.StartCoroutine(RandomTimer());
    }

    public void Tick() { }

    public void Exit()
    {
        if (_timer != null)
            _runner.StopCoroutine(_timer);
        _timer = null;
    }

    private IEnumerator RandomTimer()
    {
        float wait = Random.Range(_intervalRange.x, _intervalRange.y);
        yield return new WaitForSeconds(wait);
        _onRandomAction?.Invoke();
    }
}
