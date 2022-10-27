using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private const string Obstacle = "Obstacle";
    private const float Speed = 30;
    private const float LeftBound = -15;

    private void Update()
    {
        if (PlayerController.GameOver) return;
        transform.Translate(Time.deltaTime * Speed * Vector3.left);
        if (transform.position.x < LeftBound && gameObject.CompareTag(Obstacle))
        {
            Destroy(gameObject);
        }
    }
}