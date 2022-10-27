using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float JumpForce = 10;
    private const float GravityModifier = 1;

    private Rigidbody _playerRigidbody;
    private bool _isOnGround = true;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _playerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isOnGround = true;
    }
}