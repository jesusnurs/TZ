using System;
using StateMachine;
using UnityEngine;

public class BouncerAnimationState : IState
{
    private readonly Animator _animator;
    private readonly Action _onComplete;
    private int _animHash;
    public BouncerAnimationState(Animator animator, Action onComplete)
    {
        _animator = animator;
        _onComplete = onComplete;
    }

    public void SetAnimation(string animName)
    {
        _animHash = string.IsNullOrEmpty(animName) ? 0 : Animator.StringToHash(animName);
    }

    public void Enter()
    {
        if (_animHash == 0) { _onComplete?.Invoke(); return; }
        _animator.CrossFade(_animHash, 0f);
    }

    public void Tick()
    {
        var info = _animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1f)
            _onComplete?.Invoke();
    }

    public void Exit() { }
}
