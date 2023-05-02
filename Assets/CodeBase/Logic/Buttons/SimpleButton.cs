using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.Buttons
{
    public class SimpleButton : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnDown;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDown?.Invoke();
        }
    }
}