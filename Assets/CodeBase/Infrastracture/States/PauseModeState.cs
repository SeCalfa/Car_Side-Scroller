using CodeBase.Services.Locator;
using CodeBase.Services;
using UnityEngine;
using CodeBase.Logic.Panels;

namespace CodeBase.Infrastracture.States
{
    public class PauseModeState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly GameFactory gameFactory;
        private readonly GameObjectsLocator gameObjectsLocator;
        private readonly SceneLoader sceneLoader;

        public PauseModeState(GameStateMachine gameStateMachine, GameFactory gameFactory, GameObjectsLocator gameObjectsLocator, SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameFactory = gameFactory;
            this.gameObjectsLocator = gameObjectsLocator;
            this.sceneLoader = sceneLoader;
        }

        public void Enter(object param = null)
        {
            PausePanelCreate();
        }

        public void Exit(object param = null)
        {
            ExitPauseWay way = (ExitPauseWay)param;

            if (way == ExitPauseWay.Back)
            {

            }
            else if (way == ExitPauseWay.Restart)
            {
                ControlPanelDestroy();
                StatsPanelDestroy();
                PlayerDestroy();
                BackgroundSpawnerDestroy();
                EffectsSpawnerDestroy();
            }

            PausePanelDestroy();
            Time.timeScale = 1;
        }

        private void PausePanelCreate()
        {
            GameObject pausePanel = gameFactory.Create(Constants.PausePanelPath);

            pausePanel.GetComponent<PausePanel>().Construct(gameStateMachine, sceneLoader);

            gameObjectsLocator.Register(Constants.PausePanelName, pausePanel);
        }

        private void PausePanelDestroy()
        {
            Object.Destroy(gameObjectsLocator.GetGameObjectByName(Constants.PausePanelName));
            gameObjectsLocator.RemoveGameObject(Constants.PausePanelName);
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