using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SurvivorGame
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _stick;
        [SerializeField] private RectTransform _stickParent;

        private bool _isDragging;
        public event Action<Vector2> OnInput;
        public event Action OnInputReleased;

        private void Update()
        {
            if (_isDragging)
            {
                var stickPosition = _stickParent.InverseTransformPoint(Input.mousePosition);
                var stickParentRect = _stickParent.rect;

                var radius = stickParentRect.width / 2;
                var distance = stickPosition.magnitude;
                if (distance > radius)
                {
                    stickPosition = stickPosition.normalized * radius;
                }
                
                _stick.rectTransform.localPosition = stickPosition;
                OnInput?.Invoke(_stick.rectTransform.localPosition / radius);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnInputReleased?.Invoke();
            ResetStick();
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            ResetStick();
        }
        
        private void ResetStick()
        {
            _isDragging = false;
            _stick.rectTransform.localPosition = Vector2.zero;
        }
    }
}