using CodeBase.Logic.Entity;
using CodeBase.Services;
using CodeBase.Services.Locator;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Spawners
{
    public class EffectsSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> traps;
        [SerializeField]
        private List<GameObject> boosters;

        private GameFactory gameFactory;
        private Player player;

        public void Construct(GameFactory gameFactory, GameObjectsLocator gameObjectsLocator)
        {
            this.gameFactory = gameFactory;

            player = gameObjectsLocator.GetGameObjectByName(Constants.PlayerName).GetComponent<Player>();
        }

        public void CreateEffect()
        {
            int rand = Random.Range(0, 100);

            if(rand < 90)
            {
                // Traps
                int countRand = Random.Range(0, 2);
                bool isPlayerOnLeft = player.transform.position.x < 0 ? true : false;

                if (countRand == 0)
                {
                    if (isPlayerOnLeft)
                        gameFactory.Create(traps[Random.Range(0, traps.Count)], new Vector2(Random.Range(-1.2f, -0.7f), player.transform.position.y + 10));
                    else
                        gameFactory.Create(traps[Random.Range(0, traps.Count)], new Vector2(Random.Range(0.7f, 1.2f), player.transform.position.y + 10));
                }
                else
                {
                    if (isPlayerOnLeft)
                    {
                        gameFactory.Create(traps[Random.Range(0, traps.Count)], new Vector2(Random.Range(-1.2f, -0.7f), player.transform.position.y + 10));
                        gameFactory.Create(traps[Random.Range(0, traps.Count)], new Vector2(Random.Range(-1.2f, -0.7f) + 0.5f, player.transform.position.y + 11f));
                    }
                    else
                    {
                        gameFactory.Create(traps[Random.Range(0, traps.Count)], new Vector2(Random.Range(0.7f, 1.2f), player.transform.position.y + 10));
                        gameFactory.Create(traps[Random.Range(0, traps.Count)], new Vector2(Random.Range(0.7f, 1.2f) - 0.5f, player.transform.position.y + 11f));
                    }
                }
            }
            else if(rand >= 90)
            {
                // Boosters
                gameFactory.Create(boosters[Random.Range(0, boosters.Count)], new Vector2(Random.Range(-1.45f, 1.45f), player.transform.position.y + 10));
            }
        }
    }
}