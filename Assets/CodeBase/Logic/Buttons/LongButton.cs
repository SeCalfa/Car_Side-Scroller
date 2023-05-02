using System;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.Buttons
{
    public class LongButton : SimpleButton, IPointerUpHandler
    {
        public event Action OnUp;

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUp?.Invoke();
        }
    }
}
