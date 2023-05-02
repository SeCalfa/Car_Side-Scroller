using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.Locator
{
    public class GameObjectsLocator
    {
        private List<GameObjectsDictionary> gameObjects = new List<GameObjectsDictionary>();

        public void Register(string name, GameObject gameObject)
        {
            gameObjects.Add(new GameObjectsDictionary(name, gameObject));
        }

        public GameObject GetGameObjectByName(string name)
        {
            return gameObjects.FirstOrDefault(g => g.name == name).gameObject;
        }

        public void RemoveGameObject(string name)
        {
            GameObjectsDictionary gameObject = gameObjects.First(g => g.name == name);
            gameObjects.Remove(gameObject);
        }
    }
}
