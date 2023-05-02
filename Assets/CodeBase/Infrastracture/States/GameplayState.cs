using CodeBase.Services.Locator;
using CodeBase.Services;
using UnityEngine;
using CodeBase.Logic.Panels;
using CodeBase.Logic.Entity;

namespace CodeBase.Infrastracture.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly GameFactory gameFactory;
        private readonly GameObjectsLocator gameObjectsLocator;

        public GameplayState(GameStateMachine gameStateMachine, GameFactory gameFactory, GameObjectsLocator gameObjectsLocator)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameFactory = gameFactory;
            this.gameObjectsLocator = gameObjectsLocator;
        }

        public void Enter(object param = null)
        {
            if (param != null)
                return;

            ControlPanelCreate();
            ControlPanelConfigurate();
            StatsPanelCreate();
            StatsPanelConfigurate();
            PlayerActivate();
        }

        public void Exit(object param = null)
        {
            
        }

        private void ControlPanelCreate()
        {
            GameObject controlPanel = gameFactory.Create(Constants.ControlPanelPath);

            gameObjectsLocator.Register(Constants.ControlPanelName, controlPanel);
        }

        private void ControlPanelConfigurate()
        {
            ControlPanel controlPanel = gameObjectsLocator.GetGameObjectByName(Constants.ControlPanelName).GetComponent<ControlPanel>();
            Player player = gameObjectsLocator.GetGameObjectByName(Constants.PlayerName).GetComponent<Player>();

            controlPanel.GetGas.OnDown += player.SpeedUpOn;
            controlPanel.GetGas.OnUp += player.SpeedUpOff;
            controlPanel.GetBrake.OnDown += player.SpeedDownOn;
            controlPanel.GetBrake.OnUp += player.SpeedDownOff;
            controlPanel.GetLeft.OnDown += player.LeftOn;
            controlPanel.GetLeft.OnUp += player.LeftOff;
            controlPanel.GetRight.OnDown += player.RightOn;
            controlPanel.GetRight.OnUp += player.RightOff;
        }

        private void StatsPanelCreate()
        {
            GameObject statsPanel = gameFactory.Create(Constants.StatsPanelPath);
            statsPanel.GetComponent<StatsPanel>().GetPauseButton.OnDown += EnterPauseState;

            gameObjectsLocator.Register(Constants.StatsPanelName, statsPanel);
        }

        private void StatsPanelConfigurate()
        {
            StatsPanel statsPanel = gameObjectsLocator.GetGameObjectByName(Constants.StatsPanelName).GetComponent<StatsPanel>();
            Player player = gameObjectsLocator.GetGameObjectByName(Constants.PlayerName).GetComponent<Player>();

            player.OnCollectCoin += statsPanel.GetScorePresenter.SetScore;
            player.OnScoreAdd += statsPanel.GetScorePresenter.SetScore;
        }

        private void EnterPauseState() =>
            gameStateMachine.Enter<PauseModeState>();

        private void PlayerActivate() =>
            gameObjectsLocator.GetGameObjectByName(Constants.PlayerName).GetComponent<Player>().enabled = true;
    }
}