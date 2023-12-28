using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _animalPrefabs;
    [SerializeField] private GameObject _powerupPrefab;

    private float _spawnPosX = 14;
    private float _spawnPosZ = 19.5f;
    private float _powerupSpawnPosX = 16;

    private float _startDelay = 2;
    private float _spawnInterval = 1.5f;
    private float _powerupStartDelay = 3;
    private float _powerupSpawnInterval = 10;

    private bool _isPlayerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", _startDelay, _spawnInterval);
        InvokeRepeating("SpawnPowerupRoutine", _powerupStartDelay, _powerupSpawnInterval);
    }

    public void PlayerDead()
    {
        _isPlayerDead = true;
    }

    void SpawnRandomAnimal()
    {
        if (!_isPlayerDead)
        {
            Vector3 _spawnPosition = new Vector3(Random.Range(-_spawnPosX, _spawnPosX), 0, _spawnPosZ);
            int _animalIndex = Random.Range(0, _animalPrefabs.Length);

            Instantiate(_animalPrefabs[_animalIndex], _spawnPosition, _animalPrefabs[_animalIndex].transform.rotation);
        }
    }

    void SpawnPowerupRoutine()
    {
        if (!_isPlayerDead)
        {
            Vector3 _powerupSpawnPosition = new Vector3(Random.Range(-_powerupSpawnPosX, _powerupSpawnPosX), 1.5f, Random.Range(6f, -1.5f));
            Instantiate(_powerupPrefab, _powerupSpawnPosition, _powerupPrefab.transform.rotation);
        }      
    }
}
