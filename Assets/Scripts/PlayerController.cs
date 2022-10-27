using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool GameOver { get; private set; }

    private const string Ground = "Ground";
    private const string Obstacle = "Obstacle";
    private const float JumpForce = 700;
    private const float GravityModifier = 1.5f;
    private static readonly int JumpTrigger = Animator.StringToHash("Jump_trig");

    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;
    private bool _isOnGround = true;
    private static readonly int DeathBool = Animator.StringToHash("Death_b");
    private static readonly int DeathTypeInt = Animator.StringToHash("DeathType_int");

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        Physics.gravity *= GravityModifier;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround && !GameOver)
        {
            _playerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _isOnGround = false;
            _playerAnimator.SetTrigger(JumpTrigger);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Ground))
        {
            _isOnGround = true;
        }
        else if (collision.gameObject.CompareTag(Obstacle))
        {
            GameOver = true;
            Debug.Log("Game Over!");
            _playerAnimator.SetBool(DeathBool, true);
            _playerAnimator.SetInteger(DeathTypeInt, 1);
        }
    }
}