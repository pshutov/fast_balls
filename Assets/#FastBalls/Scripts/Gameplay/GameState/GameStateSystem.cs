using System;
using JetBrains.Annotations;

namespace _FastBalls.Gameplay.GameState
{
    [UsedImplicitly]
    public class GameStateSystem
    {
        public event Action<EGameState> StateChanged = null;
        
        
        public EGameState CurrentState { get; private set; }
        public EGameState PreviousState { get; private set; }


        public void ChangeState(EGameState state, bool force = false)
        {
            if (!force && CurrentState == state)
            {
                return;
            }
            
            SetState(state);
        }

        private void SetState(EGameState state)
        {
            PreviousState = CurrentState;
            CurrentState = state;
            
            StateChanged?.Invoke(state);
        }
    }
}