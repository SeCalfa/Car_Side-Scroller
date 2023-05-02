using System.Linq;
using UnityEngine;

namespace CodeBase.Logic.Effects
{
    public class Shield : MonoBehaviour
    {
        private string[] shieldCompetence = new string[]
        {
            Constants.BlockTag,
            Constants.CrackTag,
            Constants.OilTag
        };

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (shieldCompetence.Any(comp => collision.CompareTag(comp)))
                Destroy(collision.gameObject);
        }
    }
}