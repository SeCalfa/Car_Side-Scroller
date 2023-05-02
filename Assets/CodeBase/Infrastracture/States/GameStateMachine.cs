using CodeBase.Services;
using CodeBase.Services.Locator;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastracture.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> states;
        private IState activeState;

        public GameStateMachine(SceneLoader sceneLoader, GameFactory gameFactory, GameObjectsLocator gameObjectsLocator)
        {
            states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(PrepearState)] = new PrepearState(this, gameFactory, gameObjectsLocator),
                [typeof(GameplayState)] = new GameplayState(this, gameFactory, gameObjectsLocator),
                [typeof(PauseModeState)] = new PauseModeState(this, gameFactory, gameObjectsLocator, sceneLoader),
                [typeof(LoseModeState)] = new LoseModeState(this, gameFactory, gameObjectsLocator, sceneLoader)
            };
        }

        public void Enter<TState>(object param = null) where TState : class, IState
        {
            IState state = ChangeState<TState>(param);
            state.Enter(param);
        }

        private TState ChangeState<TState>(object param = null) where TState : class, IState
        {
            activeState?.Exit(param);

            TState state = states[typeof(TState)] as TState;
            activeState = state;

            return state;
        }
    }
}
