using UnityEngine;

namespace StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private IState _currentState;

        public void SetState(IState newState)
        {
            if (_currentState == newState) return;

            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        private void Update()
        {
            _currentState?.Tick();
        }
    }
}