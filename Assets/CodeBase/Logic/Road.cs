using CodeBase.Infrastracture.States;
using CodeBase.Logic.Entity;
using CodeBase.Services;
using CodeBase.Services.Locator;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Road : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> bushes;
        [SerializeField]
        private List<GameObject> peoples;
        [Space]
        [SerializeField]
        private GameObject startLine;
        [SerializeField]
        private Transform playerStartPoint;

        private GameStateMachine gameStateMachine;
        private GameFactory gameFactory;
        private GameObjectsLocator gameObjectsLocator;

        public void Construct(GameStateMachine gameStateMachine, GameFactory gameFactory, GameObjectsLocator gameObjectsLocator)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameFactory = gameFactory;
            this.gameObjectsLocator = gameObjectsLocator;
        }

        private void Awake()
        {
            FillRoad(bushes, 2, 4);
            FillRoad(peoples, 1, 4);
        }

        private void FillRoad(List<GameObject> list, int minSpawnNumber, int maxSpawnNumber)
        {
            int count = Random.Range(minSpawnNumber, maxSpawnNumber);

            for (int i = 0; i < count; i++)
            {
            Reset:
                int rand = Random.Range(0, list.Count);

                if (list[rand].activeSelf)
                    goto Reset;

                list[rand].SetActive(true);
            }
        }

        public void StartLineOn() =>
            startLine.SetActive(true);

        public void SpawnPlayer()
        {
            GameObject player = gameFactory.Create(Constants.PlayerPath, playerStartPoint);
            player.GetComponent<Player>().Construct(gameStateMachine, gameFactory, gameObjectsLocator);

            gameObjectsLocator.Register(Constants.PlayerName, player);
        }
    }
}