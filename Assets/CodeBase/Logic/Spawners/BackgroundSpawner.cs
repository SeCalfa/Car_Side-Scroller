using CodeBase.Services.Locator;
using CodeBase.Services;
using UnityEngine;
using CodeBase.Infrastracture.States;

namespace CodeBase.Logic.Spawners
{
    public class BackgroundSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject road;
        [Space]
        [SerializeField]
        private Sprite road1;
        [SerializeField]
        private Sprite road2;

        private float diff = 12.54f;
        private RoadType roadType = RoadType.One;

        private Vector2 roadPos = Vector2.zero;

        private GameStateMachine gameStateMachine;
        private GameFactory gameFactory;
        private GameObjectsLocator gameObjectsLocator;

        public void Construct(GameStateMachine gameStateMachine, GameFactory gameFactory, GameObjectsLocator gameObjectsLocator)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameFactory = gameFactory;
            this.gameObjectsLocator = gameObjectsLocator;

            SpawnRoad(true);
        }

        public void SpawnRoad(bool firstRoad)
        {
            GameObject current = Instantiate(road, roadPos, Quaternion.identity);
            current.GetComponent<Road>().Construct(gameStateMachine, gameFactory, gameObjectsLocator);

            if (roadType == RoadType.One)
                current.GetComponent<SpriteRenderer>().sprite = road1;
            else if (roadType == RoadType.Two)
                current.GetComponent<SpriteRenderer>().sprite = road2;

            roadPos = new Vector2(roadPos.x, roadPos.y + diff);

            if (!firstRoad)
                return;

            current.GetComponent<Road>().StartLineOn();
            current.GetComponent<Road>().SpawnPlayer();
        }
    }
}