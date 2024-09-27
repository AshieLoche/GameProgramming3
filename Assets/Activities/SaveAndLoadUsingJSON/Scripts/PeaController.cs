using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaController : MonoBehaviour
{

    #region Variable Declaration

    #region Movement
    private Rigidbody _peaRigidbody;

    [Header("Directional Movement")]
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _moveTime;
    private float _moveSpeed;
    #endregion

    #region Lifetime
    [Header("Lifetime")]
    [SerializeField] float _peaLifetime;
    #endregion

    [SerializeField] private SaveAndLoadUsingJSON _saveAndLoadUsingJSON;

    #endregion

    private void Start()
    {
        _peaRigidbody = GetComponent<Rigidbody>();
        StartCoroutine(PeaLifetime());
    }

    private void Update()
    {
        PeaMove();
    }

    private IEnumerator PeaLifetime()
    {
        yield return new WaitForSeconds(_peaLifetime);
        Destroy(gameObject);
    }

    private void PeaMove()
    {

        _moveSpeed = _moveDistance / _moveTime;
        _peaRigidbody.velocity = transform.forward * _moveSpeed;

    }

    public void SetSaveAndLoadUsingJSON(SaveAndLoadUsingJSON saveAndLoadUsingJSON)
    {
        _saveAndLoadUsingJSON = saveAndLoadUsingJSON;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
        else if (CompareTag("Player_Pea"))
        {
            if(other.CompareTag("Enemy"))
            {
                _saveAndLoadUsingJSON.RemoveEnemyTransform(other.transform);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
        else if (CompareTag("Enemy_Pea"))
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
                //Destroy(other.gameObject);
            }
        }
    }

}
