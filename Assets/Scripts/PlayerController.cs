using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;

    private float _speed = 20.0f;

    private float _xRange = 20;
    private float _zRangeMax = 6.5f;
    private float _zRangeMin = -1.5f;

    public GameObject projectilePrefab;
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // No longer necessary to Instantiate prefabs
            // Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

            // Get an object from the pool
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true); // activate it
                pooledProjectile.transform.position = transform.position; // position it at player
            }
        }
    }

    void CalculateMovement()
    {
        // Check for left and right bounds
        if (transform.position.x < -_xRange)
        {
            transform.position = new Vector3(_xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > _xRange)
        {
            transform.position = new Vector3(-_xRange, transform.position.y, transform.position.z);
        }

        // Player movement left to right
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * _speed * _horizontalInput);

        // Check for forward and backward bounds
        if (transform.position.z < _zRangeMin)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zRangeMin);
        }

        if (transform.position.z > _zRangeMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zRangeMax);
        }

        // Player movement top to down
        _verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * _speed * _verticalInput);
    }

    public IEnumerator SpeedBoostActive()
    {
        _speed = _speed + 20.0f;
        yield return new WaitForSeconds(5.0f);
        _speed = 20.0f;
    }

    private void OnDisable()
    {
        _spawnManager.PlayerDead();
    }
}
