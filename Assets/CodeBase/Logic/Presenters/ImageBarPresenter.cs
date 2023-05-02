using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.Presenters
{
    public class ImageBarPresenter : MonoBehaviour
    {
        [SerializeField]
        private Image fillImage;

        public void FillAmount(float amount)
        {
            fillImage.fillAmount = amount;
        }
    }
}