using CodeBase.Infrastracture.States;
using CodeBase.Services;
using CodeBase.Services.Locator;

namespace CodeBase.Infrastracture
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        private readonly GameFactory gameFactory;
        private readonly GameObjectsLocator gameObjectsLocator;

        public Game(ICoroutineRunner coroutineRunner)
        {
            gameFactory = new GameFactory();
            gameObjectsLocator = new GameObjectsLocator();

            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), gameFactory, gameObjectsLocator);
            StateMachine.Enter<BootstrapState>();
        }
    }
}
