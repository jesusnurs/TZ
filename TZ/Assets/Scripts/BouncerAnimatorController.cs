using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class BouncerAnimatorController : MonoBehaviour
{
    [Header("Animation Lists")]
    [SerializeField] private List<string> idleAnimations;
    [SerializeField] private List<string> randomAnimations;
    [SerializeField] private List<string> cheerAnimations;
    [SerializeField] private List<string> disappointmentAnimations;
    [SerializeField] private List<string> danceAnimations;

    [Header("Random Timing (sec)")]
    [SerializeField] private Vector2 randomInterval = new (5f, 12f);

    private Animator _animator;
    private IState _current;
    private BouncerIdleState _idle;
    private BouncerAnimationState _animation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _idle = new BouncerIdleState(_animator, this, idleAnimations, randomInterval, SwitchToRandomAction);
        _animation = new BouncerAnimationState(_animator, ReturnToIdle);
    }

    private void Start() => SetState(_idle);

    private void Update() => _current?.Tick();

    private void SetState(IState s)
    {
        _current?.Exit();
        _current = s;
        _current.Enter();
    }

    private void SwitchToRandomAction()
    {
        var name = randomAnimations is { Count: > 0 } ? randomAnimations[Random.Range(0, randomAnimations.Count)] : null;

        _animation.SetAnimation(name);
        SetState(_animation);
    }

    private void ReturnToIdle() => SetState(_idle);

    public void TriggerRandomNow() => SwitchToRandomAction();
}