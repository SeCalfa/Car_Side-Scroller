using UnityEngine;

namespace CodeBase.Logic.Entity
{
    public class Coin : MonoBehaviour
    {
        public bool isCoinChaise { get; set; }
        public Vector2 startPos { get; set; }
        public Transform target { get; set; }

        private float alpha = 0;

        private void Update()
        {
            Chaising();
        }

        private void Chaising()
        {
            if (isCoinChaise)
            {
                alpha = Mathf.Clamp01(alpha + Time.deltaTime * 2);
                transform.position = Vector2.Lerp(startPos, target.position, alpha);
            }
        }
    }
}