using UnityEngine;
using TMPro;
using CodeBase.Logic.Buttons;
using CodeBase.Infrastracture.States;
using CodeBase.Infrastracture;
using CodeBase.Services.Locator;
using CodeBase.Logic.Entity;

namespace CodeBase.Logic.Panels
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI bestScore;
        [SerializeField]
        private TextMeshProUGUI currentScore;
        [SerializeField]
        private SimpleButton restart;
        [SerializeField]
        private SimpleButton exit;

        private Animator animator;

        private int OpenHash = Animator.StringToHash("Open");

        private GameStateMachine gameStateMachine;
        private GameObjectsLocator gameObjectsLocator;
        private SceneLoader sceneLoader;

        public void Construct(GameStateMachine gameStateMachine, GameObjectsLocator gameObjectsLocator, SceneLoader sceneLoader)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameObjectsLocator = gameObjectsLocator;
            this.sceneLoader = sceneLoader;

            ScoreFill();
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.SetTrigger(OpenHash);

            restart.OnDown += Restart;
            exit.OnDown += Exit;

            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            restart.OnDown -= Restart;
            exit.OnDown -= Exit;
        }

        private void ScoreFill()
        {
            Player player = gameObjectsLocator.GetGameObjectByName(Constants.PlayerName).GetComponent<Player>();

            bestScore.text = $"Best Score: {PlayerPrefs.GetInt("Score")}";
            currentScore.text = $"Your Score: {player.score}";

            if (PlayerPrefs.GetInt("Score") < player.score)
                PlayerPrefs.SetInt("Score", player.score);
        }

        private void Restart() =>
            sceneLoader.Reset(() => gameStateMachine.Enter<PrepearState>());

        private void Exit() =>
            Application.Quit();
    }
}