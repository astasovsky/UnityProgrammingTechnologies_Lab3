using UnityEngine;

namespace Challenge_3.Scripts
{
    public class RepeatBackgroundX : MonoBehaviour
    {
        private Vector3 _startPosition;
        private float _repeatWidth;

        private void Start()
        {
            _startPosition = transform.position; // Establish the default starting position 
            _repeatWidth = GetComponent<BoxCollider>().size.x / 2; // Set repeat width to half of the background
        }

        private void Update()
        {
            // If background moves left by its repeat width, move it back to start position
            if (transform.position.x < _startPosition.x - _repeatWidth)
            {
                transform.position = _startPosition;
            }
        }
    }
}