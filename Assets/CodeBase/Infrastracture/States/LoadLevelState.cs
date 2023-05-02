using System;

namespace CodeBase.Infrastracture.States
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
        }

        public void Enter(object param = null)
        {
            string nextScene = (string)param;

            sceneLoader.Load(nextScene, OnLoaded);
        }

        public void Exit(object param = null)
        {
            
        }

        private void OnLoaded()
        {
            gameStateMachine.Enter<PrepearState>();
        }
    }
}