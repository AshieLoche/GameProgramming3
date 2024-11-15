using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;

public class SpawnerManager : MonoBehaviour
{

    #region Variable Declaration

    [Header("Player")]
    [SerializeField] private Transform _playerSpawnMarker;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerParentTransform;
    [SerializeField] private CinemachineController _cinemachineController;
    [SerializeField] private SaveAndLoadUsingJSON _saveAndLoadUsingJSON;

    [Header("Enemies")]
    [SerializeField] private List<Transform> _enemySpawnMarkers;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyParentTransform;
    private GameObject _playerClone;
    private int _enemySpawnMarkerIndex;
    private List<int> _enemySpawnMarkerIndexes;

    [Header("Peas")]
    [SerializeField] private Transform _peaParent;
    private Dictionary<GameObject, bool> reloadStates = new Dictionary<GameObject, bool>();

    #endregion

    void Start()
    {

        PlayerSpawn();
        EnemySpawn();
        
    }

    private void PlayerSpawn()
    {

        #region Instantiate Player
        _playerClone = Instantiate(_playerPrefab, _playerSpawnMarker);
        _playerClone.transform.parent = _playerParentTransform;
        _playerClone.name = "Snow Pea";
        #endregion

        #region Attach Virtual Camera
        _cinemachineController.SetVirtualCamera(_playerClone.transform);
        #endregion

        #region Set Spawn Manager
        _playerClone.GetComponent<PlayerController>().SetSpawnManager(this);
        #endregion

        _saveAndLoadUsingJSON.SetPlayerTransform(_playerClone.transform);

    }

    private void EnemySpawn()
    {

        _enemySpawnMarkerIndexes = new List<int>();
        int enemyCount = 0;
        GameObject enemyClone;

        for (int i = 0; i < _enemySpawnMarkers.Count; i++)
        {

            _enemySpawnMarkerIndex = Random.Range(0, _enemySpawnMarkers.Count);

            if (!_enemySpawnMarkerIndexes.Contains(_enemySpawnMarkerIndex))
            {
                _enemySpawnMarkerIndexes.Add(_enemySpawnMarkerIndex);
                enemyClone = Instantiate(_enemyPrefab, _enemySpawnMarkers[_enemySpawnMarkerIndex].position, Quaternion.Euler(transform.up * Random.Range(0f, 360f)));
                enemyClone.transform.parent = _enemyParentTransform;
                enemyClone.name = "Peashooter_" + (i + 1);
                EnemyController enemyController = enemyClone.GetComponent<EnemyController>();
                enemyController.SetSpawnManager(this);
                enemyController.SetPlayerTransform(_playerClone.transform);
                enemyController.SetSaveAndLoadUsingJSON(_saveAndLoadUsingJSON);
                enemyCount++;
            }
            else
            {
                i--;
            }

            if (enemyCount == 10)
            {
                break;
            }

        }

    }

    public void PeaSpawn(GameObject peaPrefab, Transform peaSpawnMarker, float reloadTime, GameObject user)
    {
        if (reloadStates.ContainsKey(user))
        {
            if (!reloadStates[user])
            {
                StartCoroutine(PeaSpawnReload(reloadTime, user));
                GameObject pea = Instantiate(peaPrefab, peaSpawnMarker);
                pea.transform.parent = _peaParent;
                pea.GetComponent<PeaController>().SetSaveAndLoadUsingJSON(_saveAndLoadUsingJSON);
            }
        }
        else
        {
            reloadStates.Add(user, false);
        }

    }

    private IEnumerator PeaSpawnReload(float reloadTime, GameObject user)
    {
        reloadStates[user] = true;
        yield return new WaitForSeconds(reloadTime);
        reloadStates[user] = false;
    }

    public void LoadPlayer(Vector3 position, Quaternion rotation)
    {
        _playerClone.transform.position = position;
        _playerClone.transform.rotation = rotation;
    }

    public void LoadEnemies(string name, Vector3 position, Quaternion rotation)
    {
        Transform child;

        for (int i = 0; i < _enemyParentTransform.childCount; i++)
        {
            child = _enemyParentTransform.GetChild(i);

            if (child.name == name)
            {
                child.position = position;
                child.rotation = rotation;
            }
        }
    }

    public void RespawnEnemies(string name, Vector3 position, Quaternion rotation)
    {
        GameObject enemyClone = Instantiate(_enemyPrefab, position, rotation);
        enemyClone.transform.parent = _enemyParentTransform;
        enemyClone.name = name;
        EnemyController enemyController = enemyClone.GetComponent<EnemyController>();
        enemyController.SetSpawnManager(this);
        enemyController.SetPlayerTransform(_playerClone.transform);
        enemyController.SetSaveAndLoadUsingJSON(_saveAndLoadUsingJSON);
    }

    public void DestroyEnemies(string name)
    {
        Transform child;

        for (int i = 0; i < _enemyParentTransform.childCount; i++)
        {
            child = _enemyParentTransform.GetChild(i);

            if (child.name == name)
            {
                Destroy(child.gameObject);
            }
        }
    }

}
