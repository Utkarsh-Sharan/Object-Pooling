using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _rotationSpeed = 50;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        Destroy(this.gameObject, 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.SpeedBoostActive();
            Destroy(this.gameObject);
        }
    }
}
