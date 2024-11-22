using UnityEngine;

public class Week12_PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed;
    private float _xMove, _zMove;
    private Vector3 _move;

    private void Update()
    {
        _xMove = Input.GetAxis("Horizontal");
        _zMove = Input.GetAxis("Vertical");
        
        _move = new Vector3(_xMove, 0f,_zMove);
        transform.Translate(_speed * Time.deltaTime * _move);
    }

}