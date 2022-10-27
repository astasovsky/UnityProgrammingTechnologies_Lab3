using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    private const float StartDelay = 2;
    private const float RepeatRate = 2;
    private readonly Vector3 _spawnPosition = new(25, 0, 0);

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), StartDelay, RepeatRate);
    }

    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, _spawnPosition, obstaclePrefab.transform.rotation);
    }
}