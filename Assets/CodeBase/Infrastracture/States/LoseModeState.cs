using CodeBase.Logic.Panels;
using CodeBase.Services;
using CodeBase.Services.Locator;
using UnityEngine;

namespace CodeBase.Infrastracture.States
{
    public class LoseModeState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly GameFactory gameFactory;
        private readonly GameObjectsLocator gameObjectsLocator;
        private readonly SceneLoader sceneLoader;

        public LoseModeState(GameStateMachine gameStateMachine, GameFactory gameFactory, GameObjectsLocator gameObjectsLocator, SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameFactory = gameFactory;
            this.gameObjectsLocator = gameObjectsLocator;
            this.sceneLoader = sceneLoader;
        }

        public void Enter(object param = null)
        {
            LosePanelCreate();
        }

        public void Exit(object param = null)
        {
            Time.timeScale = 1;

            LosePanelDestroy();
            ControlPanelDestroy();
            StatsPanelDestroy();
            PlayerDestroy();
            BackgroundSpawnerDestroy();
            EffectsSpawnerDestroy();
        }

        private void LosePanelCreate()
        {
            GameObject losePanel = gameFactory.Create(Constants.LosePanelPath);
            losePanel.GetComponent<LosePanel>().Construct(gameStateMachine, gameObjectsLocator, sceneLoader);

            gameObjectsLocator.Register(Constants.LosePanelName, losePanel);
        }

        private void LosePanelDestroy()
        {
            Object.Destroy(gameObjectsLocator.GetGameObjectByName(Constants.LosePanelName));
            gameObjectsLocator.RemoveGameObject(Constants.LosePanelName);
        }

        private void ControlPanelDestroy()
        {
            Object.Destroy(gameObjectsLocator.GetGameObjectByName(Constants.ControlPanelName));
            gameObjectsLocator.RemoveGameObject(Constants.ControlPanelName);
        }

        private void StatsPanelDestroy()
        {
            Object.Destroy(gameObjectsLocator.GetGameObjectByName(Constants.StatsPanelName));
            gameObjectsLocator.RemoveGameObject(Constants.StatsPanelName);
        }

        private void PlayerDestroy()
        {
            Object.Destroy(gameObjectsLocator.GetGameObjectByName(Constants.PlayerName));
            gameObjectsLocator.RemoveGameObject(Constants.PlayerName);
        }

        private void BackgroundSpawnerDestroy()
        {
            Object.Destroy(gameObjectsLocator.GetGameObjectByName(Constants.BackgroundSpawnerName));
            gameObjectsLocator.RemoveGameObject(Constants.BackgroundSpawnerName);
        }

        private void EffectsSpawnerDestroy()
        {
            Object.Destroy(gameObjectsLocator.GetGameObjectByName(Constants.EffectsSpawnerName));
            gameObjectsLocator.RemoveGameObject(Constants.EffectsSpawnerName);
        }
    }
}