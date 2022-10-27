using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;

    private const float StartDelay = 2;
    private const float RepeatRate = 2;
    private readonly Vector3 _spawnPosition = new(25, 0, 0);
    private int _randomObstacle;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), StartDelay, RepeatRate);
    }

    private void SpawnObstacle()
    {
        if (PlayerController.GameOver) return;
        _randomObstacle = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[_randomObstacle], _spawnPosition,
            obstaclePrefabs[_randomObstacle].transform.rotation);
    }
}