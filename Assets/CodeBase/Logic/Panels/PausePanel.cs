using CodeBase.Infrastracture;
using CodeBase.Infrastracture.States;
using CodeBase.Logic.Buttons;
using UnityEngine;

namespace CodeBase.Logic.Panels
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField]
        private SimpleButton back;
        [SerializeField]
        private SimpleButton restart;
        [SerializeField]
        private SimpleButton exit;

        private Animator animator;

        private int OpenHash = Animator.StringToHash("Open");
        private int HideHash = Animator.StringToHash("Hide");

        private GameStateMachine gameStateMachine;
        private SceneLoader sceneLoader;

        public void Construct(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.SetTrigger(OpenHash);

            back.OnDown += Close;
            restart.OnDown += Restart;
            exit.OnDown += Exit;

            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            back.OnDown -= Close;
            restart.OnDown -= Restart;
            exit.OnDown -= Exit;
        }

        private void Close() =>
            animator.SetTrigger(HideHash);

        private void Restart() =>
            sceneLoader.Reset(() => gameStateMachine.Enter<PrepearState>(ExitPauseWay.Restart));

        private void Exit() =>
            Application.Quit();

        private void HideEnd() =>
            gameStateMachine.Enter<GameplayState>(ExitPauseWay.Back);
    }
}