using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Rigidbody _player;
    [SerializeField] private float _distance, _time, _timer;
    private Camera _mainCamera;
    private Vector2 _moveDirection;
    private float _moveSpeed;
    private bool _isFiring;

    private void Start()
    {

        _mainCamera = Camera.main;
        _player = GetComponent<Rigidbody>();
        _inputReader.MoveEvent += HandleMove;
        _inputReader.FireEvent += HandleFire;
        _inputReader.FireCanceledEvent += HandleFireCancel;
        _moveSpeed = _distance / _time;
        
    }

    private void Update()
    {
        //Rotate();
        Move();
        Fire();
    }

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

    private void Rotate()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward * _moveDirection.y + transform.right * _moveDirection.x;

        _player.velocity = moveDirection * _moveSpeed;
    }

    private void Fire()
    {

    }

}
