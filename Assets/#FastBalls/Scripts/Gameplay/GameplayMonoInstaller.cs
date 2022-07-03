using _CrossProjects.Tools.Coroutines;
using _FastBalls.Gameplay.Configs;
using _FastBalls.Gameplay.GameState;
using UnityEngine;
using Zenject;

namespace _FastBalls.Gameplay
{
    public sealed class GameplayMonoInstaller : MonoInstaller
    {
        [Header("CONFIGS")]
        [SerializeField]
        private LevelConfig m_levelConfig = null;
        
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameplaySystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<GameStateSystem>().FromNew().AsSingle().NonLazy();
            
            Container.BindInstance(m_levelConfig).AsSingle().NonLazy();
            
            Container.Bind<LevelCoroutinesProvider>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}
