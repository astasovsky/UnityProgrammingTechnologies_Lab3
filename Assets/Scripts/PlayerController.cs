using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool GameOver { get; private set; }

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;

    private const string Ground = "Ground";
    private const string Obstacle = "Obstacle";
    private const string RunningJump = "Running_Jump";
    private const float JumpForce = 700;
    public float doubleJumpForce = 400;
    private const float GravityModifier = 1.5f;
    private static readonly int JumpTrigger = Animator.StringToHash("Jump_trig");
    private static readonly int DeathBool = Animator.StringToHash("Death_b");
    private static readonly int DeathTypeInt = Animator.StringToHash("DeathType_int");

    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;
    private AudioSource _playerAudio;
    private bool _isOnGround = true;
    public bool doubleJumpUsed = false;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= GravityModifier;
    }

    private void Update()
    {
        if (GameOver) return;
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            dirtParticle.Stop();
            _playerAudio.PlayOneShot(jumpSound);
            _playerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _isOnGround = false;
            _playerAnimator.SetTrigger(JumpTrigger);
            doubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !_isOnGround && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            _playerRigidbody.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            _playerAnimator.Play(RunningJump, 3, 0f);
            _playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameOver) return;
        if (collision.gameObject.CompareTag(Ground))
        {
            dirtParticle.Play();
            _isOnGround = true;
        }
        else if (collision.gameObject.CompareTag(Obstacle))
        {
            dirtParticle.Stop();
            _playerAudio.PlayOneShot(crashSound);
            explosionParticle.Play();
            GameOver = true;
            Debug.Log("Game Over!");
            _playerAnimator.SetBool(DeathBool, true);
            _playerAnimator.SetInteger(DeathTypeInt, 1);
        }
    }
}