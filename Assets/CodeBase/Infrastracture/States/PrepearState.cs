using CodeBase.Logic.Spawners;
using CodeBase.Logic.Timer;
using CodeBase.Services;
using CodeBase.Services.Locator;
using UnityEngine;

namespace CodeBase.Infrastracture.States
{
    public class PrepearState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly GameFactory gameFactory;
        private readonly GameObjectsLocator gameObjectsLocator;

        public PrepearState(GameStateMachine gameStateMachine, GameFactory gameFactory, GameObjectsLocator gameObjectsLocator)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameFactory = gameFactory;
            this.gameObjectsLocator = gameObjectsLocator;
        }

        public void Enter(object param = null)
        {
            TimerToStartCreate();
            BackgroundSpawnerCreate();
            EffectsSpawnerCreate();
        }

        public void Exit(object param = null)
        {
            TimerToStartDestroy();
        }

        private void TimerToStartCreate()
        {
            GameObject timerToStart = gameFactory.Create(Constants.TimerToStartPath);
            timerToStart.GetComponent<TimerToStart>().Construct(gameStateMachine);

            gameObjectsLocator.Register(Constants.TimerToStartName, timerToStart);
        }

        private void BackgroundSpawnerCreate()
        {
            GameObject backgroundSpawner = gameFactory.Create(Constants.BackgroundSpawnerPath);
            backgroundSpawner.GetComponent<BackgroundSpawner>().Construct(gameStateMachine, gameFactory, gameObjectsLocator);

            gameObjectsLocator.Register(Constants.BackgroundSpawnerName, backgroundSpawner);
        }

        private void EffectsSpawnerCreate()
        {
            GameObject effectsSpawner = gameFactory.Create(Constants.EffectsSpawnerPath);
            effectsSpawner.GetComponent<EffectsSpawner>().Construct(gameFactory, gameObjectsLocator);

            gameObjectsLocator.Register(Constants.EffectsSpawnerName, effectsSpawner);
        }

        private void TimerToStartDestroy()
        {
            Object.Destroy(gameObjectsLocator.GetGameObjectByName(Constants.TimerToStartName));
            gameObjectsLocator.RemoveGameObject(Constants.TimerToStartName);
        }
    }
}