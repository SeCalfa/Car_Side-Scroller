using System;

namespace CodeBase.Infrastracture.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
        }

        public void Enter(object param = null)
        {
            sceneLoader.Load(Constants.InitScene, LoadGameLevel);
        }

        public void Exit(object param = null)
        {
            
        }

        private void LoadGameLevel()
        {
            gameStateMachine.Enter<LoadLevelState>(Constants.GameScene);
        }
    }
}