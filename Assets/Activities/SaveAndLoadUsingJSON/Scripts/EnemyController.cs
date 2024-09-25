using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //[SerializeField] private Transform _player;
    //[SerializeField] private float _fireDistance, _fireTime, _fireRotationAngle, _fireRotationTime;
    //[SerializeField] private GameObject _pea;
    //[SerializeField] private Transform _peaMarker;
    //[SerializeField] private Transform _peas;
    //private float _fireSpeed, _fireRotationSpeed;
    //private bool _isReloading;

    #region Variable Declaration

    #region Movement
    private Rigidbody _enemyRigidbody;
    [SerializeField] private Transform _playerTransform;

    [Header("Rotational Movement")]
    [SerializeField] private float _rotationAngle;
    [SerializeField] private float _rotationTime;
    private float _rotationDirection, _rotationSpeed;
    #endregion

    #region Pea Fire
    [Header("Pea Firing")]
    [SerializeField] private GameObject _enemyPeaPrefab;
    [SerializeField] private Transform _enemyPeaSpawnMarker;
    [SerializeField] private float _reloadTime;
    [SerializeField] private SpawnerManager _spawnerManager;
    private bool _isFiring;
    #endregion

    #endregion

    void Start()
    {

        _enemyRigidbody = GetComponent<Rigidbody>();
        //_fireSpeed = _fireDistance / _fireTime;
        //_fireRotationSpeed = _fireRotationAngle / _fireRotationTime;

    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {

        _rotationSpeed = _rotationAngle / _rotationTime;
        Vector3 distanceBetween = (_playerTransform.position - transform.position).normalized; // Direction according to Applied Game Physics
        _rotationDirection = Mathf.Sqrt(Mathf.Pow(distanceBetween.x, 2) + Mathf.Pow(distanceBetween.z, 2));
        _enemyRigidbody.angularVelocity = new Vector3(0f, distanceBetween.z * _rotationDirection, 0f);

    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }
    public void SetSpawnManager(SpawnerManager spawnerManager)
    {
        _spawnerManager = spawnerManager;
    }

}
