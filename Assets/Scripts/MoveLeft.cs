using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private const float Speed = 30;

    private void Update()
    {
        transform.Translate(Time.deltaTime * Speed * Vector3.left);
    }
}