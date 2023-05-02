using UnityEngine;

namespace CodeBase.Services.Locator
{
    public class GameObjectsDictionary
    {
        public string name { get; }
        public GameObject gameObject { get; }

        public GameObjectsDictionary(string name, GameObject gameObject)
        {
            this.name = name;
            this.gameObject = gameObject;
        }
    }
}
