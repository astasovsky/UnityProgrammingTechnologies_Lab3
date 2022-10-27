using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool GameOver { get; private set; } = true;
    public static bool DoubleSpeed { get; private set; }

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private Transform startingPoint;

    private const string Ground = "Ground";
    private const string Obstacle = "Obstacle";
    private const string RunningJump = "Running_Jump";
    private const float JumpForce = 700;
    private const float DoubleJumpForce = 400;
    private const float GravityModifier = 1.5f;
    private const float LerpSpeed = 5;
    private static readonly int JumpTrigger = Animator.StringToHash("Jump_trig");
    private static readonly int DeathBool = Animator.StringToHash("Death_b");
    private static readonly int DeathTypeInt = Animator.StringToHash("DeathType_int");
    private static readonly int SpeedMultiplier = Animator.StringToHash("Speed_Multiplier");

    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;
    private AudioSource _playerAudio;
    private bool _isOnGround = true;
    private bool _doubleJumpUsed = false;

    public void PlayIntro()
    {
        StartCoroutine(PlayIntroCoroutine());
    }

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
            _doubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !_isOnGround && !_doubleJumpUsed)
        {
            _doubleJumpUsed = true;
            _playerRigidbody.AddForce(Vector3.up * DoubleJumpForce, ForceMode.Impulse);
            _playerAnimator.Play(RunningJump, 3, 0f);
            _playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        if (Input.GetKey(KeyCode.LeftShift) && _isOnGround)
        {
            DoubleSpeed = true;
            _playerAnimator.SetFloat(SpeedMultiplier, 2.0f);
        }
        else if (DoubleSpeed)
        {
            DoubleSpeed = false;
            _playerAnimator.SetFloat(SpeedMultiplier, 1.0f);
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

    private IEnumerator PlayIntroCoroutine()
    {
        GameOver = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * LerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        _playerAnimator.SetFloat(SpeedMultiplier, 0.5f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * LerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        _playerAnimator.SetFloat(SpeedMultiplier, 1.0f);
        GameOver = false;
    }
}