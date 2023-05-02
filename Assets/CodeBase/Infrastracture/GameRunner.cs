using UnityEngine;

namespace CodeBase.Infrastracture
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField]
        private GameObject bootstrapperPrefab;

        private void Awake()
        {
            GameBootstraper bootstrapper = FindObjectOfType<GameBootstraper>();

            if (bootstrapper == null)
                Instantiate(bootstrapperPrefab);
        }
    }
}