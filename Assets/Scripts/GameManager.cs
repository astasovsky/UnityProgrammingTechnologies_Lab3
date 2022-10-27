using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float _score = 0;

    private void Update()
    {
        if (PlayerController.GameOver) return;
        if (PlayerController.DoubleSpeed)
        {
            _score += 2;
        }
        else
        {
            _score++;
        }

        Debug.Log("Score: " + _score);
    }
}