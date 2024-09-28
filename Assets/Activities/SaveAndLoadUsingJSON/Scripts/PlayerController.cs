using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Variable Declaration

    #region Input Event Reader
    [Header("Input Reader")]
    [SerializeField] private InputReader _inputReader;
    #endregion

    #region Movement
    private Rigidbody _playerRigidbody;

    [Header("Directional Movement")]
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _mouseSensitivity;
    private Vector2 _moveDirection;
    private float _moveSpeed;

    [Header("Rotational Movement")]
    [SerializeField] private float _rotationAngle;
    [SerializeField] private float _rotationTime;
    private float _rotationDirection, _rotationSpeed;
    #endregion

    #region Pea Fire
    [Header("Pea Firing")]
    [SerializeField] private GameObject _playerPeaPrefab;
    [SerializeField] private Transform _playerPeaSpawnMarker;
    [SerializeField] private float _reloadTime;
    [SerializeField] private SpawnerManager _spawnerManager;
    private bool _isFiring;
    #endregion

    #endregion

    private void Start()
    {

        #region Input Event Handler
        _inputReader.MoveEvent += HandleMove;
        _inputReader.FireEvent += HandleFire;
        _inputReader.FireCanceledEvent += HandleFireCancel;
        #endregion

        #region Disappear Cursor
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        #endregion

        #region Rigidbody Initialization
        _playerRigidbody = GetComponent<Rigidbody>();
        #endregion

    }

    private void Update()
    {
        Rotate();
        Move();
        Fire();
    }

    #region Input Event Handler
    private void HandleMove(Vector2 dir)
    {
        _moveDirection = dir;
    }

    private void HandleFire()
    {
        _isFiring = true;
    }

    private void HandleFireCancel()
    {
        _isFiring = false;
    }
    #endregion

    #region Movement
    private void Rotate()
    {

        _rotationSpeed = _rotationAngle / _rotationTime;
        //_rotationDirection = Input.GetAxis("Mouse X");
        Debug.Log(_rotationDirection);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rotationDirection = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _rotationDirection = 1f;
        }
        else
        {
            _rotationDirection = 0f;
        }

        _playerRigidbody.angularVelocity = new Vector3(0f, _rotationSpeed * _rotationDirection, 0f);

    }

    private void Move()
    {

        _moveSpeed = _moveDistance / _moveTime;
        Vector3 localizedMoveDirection = transform.forward * _moveDirection.y + transform.right * _moveDirection.x;
        _playerRigidbody.velocity = localizedMoveDirection * _moveSpeed;

    }
    #endregion

    #region Pea Fire
    public void SetSpawnManager(SpawnerManager spawnerManager)
    {
        _spawnerManager = spawnerManager;
    }

    private void Fire()
    {

        if (_isFiring)
        {
            _spawnerManager.PeaSpawn(_playerPeaPrefab, _playerPeaSpawnMarker,_reloadTime, gameObject);
        }

    }
    #endregion

}
