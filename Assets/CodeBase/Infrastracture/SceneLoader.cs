using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastracture
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }

        public void Load(string levelName, Action onLoaded = null) =>
            coroutineRunner.StartCoroutine(LoadScene(levelName, onLoaded));

        public void Reset(Action onLoaded = null) =>
            coroutineRunner.StartCoroutine(ResetScene(onLoaded));

        private IEnumerator LoadScene(string nextLevel, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextLevel)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextLevel);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }

        private IEnumerator ResetScene(Action onLoaded = null)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(currentScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
