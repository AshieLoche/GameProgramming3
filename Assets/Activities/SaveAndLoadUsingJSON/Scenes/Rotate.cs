using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private float _rotationAngle, _rotationTime;
    [SerializeField] private float _rotationDirection, _rotationSpeed;
    [SerializeField] private Rigidbody _enemyRigidbody;
    [SerializeField] private Vector3 _distanceBetween;
    [SerializeField] private Quaternion _rotation;
    public float angle;

    private void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        // Get the direction vector from the enemy to the player
        Vector3 directionToPlayer = _player.position - transform.position;
        directionToPlayer.y = 0; // Keep the rotation in the horizontal plane

        // Normalize the direction vector
        if (directionToPlayer.magnitude > 0) // Avoid division by zero
        {
            directionToPlayer.Normalize();
        }

        // Calculate the angle between the enemy's forward direction and the direction to the player
        float angleToPlayer = Vector3.SignedAngle(transform.forward, directionToPlayer, Vector3.up);

        // Calculate the desired angular velocity based on the angle
        _rotationSpeed = _rotationAngle / _rotationTime; // Set your desired rotation speed
        _enemyRigidbody.angularVelocity = new Vector3(0f, angleToPlayer * _rotationSpeed, 0f);

    }
}
