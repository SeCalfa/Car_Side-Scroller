using UnityEngine;

namespace CodeBase.Services
{
    public class GameFactory
    {
        public GameObject Create(string path)
        {
            GameObject gameObject = Resources.Load(path) as GameObject;
            GameObject gameObjectSpawned = Object.Instantiate(gameObject);

            return gameObjectSpawned;
        }

        public GameObject Create(string path, Transform at)
        {
            GameObject gameObject = Resources.Load(path) as GameObject;
            GameObject gameObjectSpawned = Object.Instantiate(gameObject);

            gameObjectSpawned.transform.position = at.position;
            gameObjectSpawned.transform.rotation = at.rotation;
            gameObjectSpawned.transform.localScale = at.lossyScale;

            return gameObjectSpawned;
        }

        public GameObject Create(GameObject gameObject, Vector2 pos)
        {
            GameObject gameObjectSpawned = Object.Instantiate(gameObject);

            gameObjectSpawned.transform.position = pos;

            return gameObjectSpawned;
        }
    }
}