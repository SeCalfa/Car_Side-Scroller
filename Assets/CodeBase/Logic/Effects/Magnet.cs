using CodeBase.Logic.Entity;
using UnityEngine;

namespace CodeBase.Logic.Effects
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Constants.CoinTag))
            {
                collision.GetComponent<Coin>().target = playerTransform;
                collision.GetComponent<Coin>().startPos = collision.transform.position;
                collision.GetComponent<Coin>().isCoinChaise = true;
            }
        }
    }
}