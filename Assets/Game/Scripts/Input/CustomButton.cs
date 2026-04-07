using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.InputSystem
{
    public sealed class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action OnUp { get; set; }
        public Action OnDown { get; set; }
        
        public void OnPointerDown(PointerEventData eventData) => OnDown?.Invoke();

        public void OnPointerUp(PointerEventData eventData) => OnUp?.Invoke();
    }
}