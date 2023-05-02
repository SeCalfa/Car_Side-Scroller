using UnityEngine;

namespace CodeBase.Infrastracture
{
    public class GameBootstraper : MonoBehaviour, ICoroutineRunner
    {
        private Game game;

        private void Awake()
        {
            game = new Game(this);
            FirstOpen();

            DontDestroyOnLoad(this);
        }

        private void FirstOpen()
        {
            if (!PlayerPrefs.HasKey("Score"))
                PlayerPrefs.SetInt("Score", 0);
        }
    }
}