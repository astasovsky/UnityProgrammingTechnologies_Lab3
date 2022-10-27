using UnityEngine;

namespace Challenge_3.Scripts
{
    public class PlayerControllerX : MonoBehaviour
    {
        public bool gameOver;

        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private ParticleSystem fireworksParticle;
        [SerializeField] private AudioClip moneySound;
        [SerializeField] private AudioClip explodeSound;
        [SerializeField] private AudioClip groundSound;

        private const float FloatForce = 500;
        private const float GravityModifier = 1.5f;
        private const string Bomb = "Bomb";
        private const string Money = "Money";
        private const string Ground = "Ground";

        private Rigidbody _playerRigidbody;
        private AudioSource _playerAudioSource;

        // Start is called before the first frame update
        private void Start()
        {
            Physics.gravity *= GravityModifier;
            _playerAudioSource = GetComponent<AudioSource>();
            _playerRigidbody = GetComponent<Rigidbody>();
            // Apply a small upward force at the start of the game
            _playerRigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }

        // Update is called once per frame
        private void Update()
        {
            // While space is pressed and player is low enough, float up
            if (Input.GetKeyDown(KeyCode.Space) && !gameOver && transform.position.y < 13)
            {
                _playerRigidbody.AddForce(Vector3.up * FloatForce);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            // if player collides with bomb, explode and set gameOver to true
            if (other.gameObject.CompareTag(Bomb))
            {
                explosionParticle.Play();
                _playerAudioSource.PlayOneShot(explodeSound, 1.0f);
                gameOver = true;
                Debug.Log("Game Over!");
                Destroy(other.gameObject);
            }

            // if player collides with money, fireworks
            else if (other.gameObject.CompareTag(Money))
            {
                fireworksParticle.Play();
                _playerAudioSource.PlayOneShot(moneySound, 1.0f);
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag(Ground))
            {
                _playerAudioSource.PlayOneShot(groundSound);
                _playerRigidbody.AddForce(Vector3.up * FloatForce);
            }
        }
    }
}