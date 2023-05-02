using UnityEngine;
using TMPro;

namespace CodeBase.Logic.Presenters
{
    public class ScorePresenter : MonoBehaviour
    {
        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void SetScore(int score)
        {
            text.text = $"Score: {score}";
        }
    }
}