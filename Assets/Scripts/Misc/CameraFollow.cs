using UnityEngine;
using Zenject;

namespace SurvivorGame
{
    public class CameraFollow : MonoBehaviour
    {
        [Inject] private PlayerView target; 
        [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); 
        [SerializeField] private float smoothSpeed = 0.125f; 

        private void LateUpdate()
        {
            if (target == null) return;

            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}