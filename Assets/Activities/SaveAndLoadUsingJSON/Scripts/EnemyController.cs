using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{

    #region Variable Declaration

    #region Movement
    private Rigidbody _enemyRigidbody;
    [SerializeField] private Transform _playerTransform;

    [Header("Rotational Movement")]
    [SerializeField] private float _reachLength;
    [SerializeField] private float _sightLength;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private float _rotationTime;
    private float _rotationDirection, _rotationSpeed;
    private Vector3 _origin, _direction, _normalizedOrigin, _normalizedDirection;
    private float _originMagnitude, _directionMagnitude;
    private float _dotProduct;
    private Vector3 _crossProduct, _normalizedCrossProduct;
    private float _crossProductMagnitude;
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
        _enemyPeaSpawnMarker = transform.GetChild(1);

    }

    void Update()
    {
        if (_playerTransform != null)
        {
            Rotate();
            Fire();
        }
        else
        {
            _enemyRigidbody.angularVelocity = Vector3.zero;
        }
    }

    #region Movement
    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void Rotate()
    {

        if (InPeripheral() && !PlayerHidden())
        {
            _crossProduct = GetCrossProduct(_origin, _direction);
            _crossProductMagnitude = GetMagnitude(_crossProduct);
            _normalizedCrossProduct = GetNormalized(_crossProduct, _crossProductMagnitude);

            _rotationDirection = _normalizedCrossProduct.y;
            _rotationSpeed = _rotationAngle / _rotationTime;
            _enemyRigidbody.angularVelocity = _rotationDirection * _rotationSpeed * 180f * Vector3.up;
        }
        else
        {
            _enemyRigidbody.angularVelocity = Vector3.zero;
        }

    }

    private bool InPeripheral()
    {

        GetVectors();

        _originMagnitude = GetMagnitude(_origin);
        _directionMagnitude = GetMagnitude(_direction);

        _normalizedOrigin = GetNormalized(_origin, _originMagnitude);
        _normalizedDirection = GetNormalized(_direction, _directionMagnitude);

        _dotProduct = GetDotProduct();

        return _dotProduct >= -0.25f && _dotProduct <= 0.99f && _directionMagnitude < _sightLength;

    }

    private void GetVectors()
    {
        _origin = transform.forward;
        _direction = _playerTransform.position - _enemyPeaSpawnMarker.position;
    }

    private float GetMagnitude(Vector3 vector)
    {
        return Mathf.Sqrt(
            (vector.x * vector.x) +
            (vector.y * vector.y) +
            (vector.z * vector.z));
    }

    private Vector3 GetNormalized(Vector3 vector, float magnitude)
    {
        return vector / magnitude;
    }

    private float GetDotProduct()
    {
        return (_normalizedOrigin.x * _normalizedDirection.x) +
            (_normalizedOrigin.y * _normalizedDirection.y) +
            (_normalizedOrigin.z * _normalizedDirection.z);
    }

    private Vector3 GetCrossProduct(Vector3 vectorA, Vector3 vectorB)
    {
        return new Vector3(
                vectorA.y * vectorB.z - vectorA.z * vectorB.y,
                vectorA.z * vectorB.x - vectorA.x * vectorB.z,
                vectorA.x * vectorB.y - vectorA.y * vectorB.x);
    }
    #endregion

    #region Pea Fire
    public void SetSpawnManager(SpawnerManager spawnerManager)
    {
        _spawnerManager = spawnerManager;
    }

    private void Fire()
    {
        if (ReachPlayer())
        {
            _spawnerManager.PeaSpawn(_enemyPeaPrefab, _enemyPeaSpawnMarker, _reloadTime, gameObject);
        }
    }
    private bool ReachPlayer()
    {

        return (!PlayerHidden()) ? Physics.Raycast(_enemyPeaSpawnMarker.position, transform.forward, _reachLength, 1 << LayerMask.NameToLayer("Player")) : false;

    }

    private bool PlayerHidden()
    {

        if (Physics.Raycast(_enemyPeaSpawnMarker.position, _direction, out RaycastHit hit, _sightLength))
        {

            return LayerMask.LayerToName(hit.collider.gameObject.layer) != "Player";

        }

        return false;

    }

    private void OnDrawGizmos()
    {
        // Reach
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_enemyPeaSpawnMarker.position, _enemyPeaSpawnMarker.position + (transform.forward * _reachLength));

        // Visibility
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_enemyPeaSpawnMarker.position, _enemyPeaSpawnMarker.position + _direction);

        // Sight
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_enemyPeaSpawnMarker.position + (transform.forward * _reachLength), _enemyPeaSpawnMarker.position + (transform.forward * _reachLength) + (transform.forward * _sightLength));
    }
    #endregion

}
