using UnityEngine;

namespace Challenge_3.Scripts
{
    public class MoveLeftX : MonoBehaviour
    {
        [SerializeField] private float speed;

        private const float LeftBound = -10;
        private const string Player = "Player";
        private const string Background = "Background";

        private PlayerControllerX _playerControllerScript;

        // Start is called before the first frame update
        private void Start()
        {
            _playerControllerScript = GameObject.Find(Player).GetComponent<PlayerControllerX>();
        }

        // Update is called once per frame
        private void Update()
        {
            // If game is not over, move to the left
            if (!_playerControllerScript.gameOver)
            {
                transform.Translate(speed * Time.deltaTime * Vector3.left, Space.World);
            }

            // If object goes off screen that is NOT the background, destroy it
            if (transform.position.x < LeftBound && !gameObject.CompareTag(Background))
            {
                Destroy(gameObject);
            }
        }
    }
}