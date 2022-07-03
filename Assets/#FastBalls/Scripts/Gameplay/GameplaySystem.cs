using _FastBalls.Gameplay.GameState;
using Zenject;

namespace _FastBalls.Gameplay
{
    public class GameplaySystem : IInitializable
    {
        private readonly GameStateSystem m_gameStateSystem = null;
        
        
        [Inject]
        public GameplaySystem(
            GameStateSystem gameStateSystem
        )
        {
            m_gameStateSystem = gameStateSystem;
        }
        
        
        void IInitializable.Initialize()
        {
            m_gameStateSystem.ChangeState(EGameState.LEVEL_START);
        }
    }
}