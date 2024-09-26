using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    #region Variable Declaration

    [Header("Player")]
    [SerializeField] private Transform _playerSpawnMarker;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerParentTransform;
    [SerializeField] private CinemachineController cinemachineController;

    [Header("Enemies")]
    [SerializeField] private List<Transform> _enemySpawnMarkers;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyParentTransform;
    private GameObject _playerClone;
    private int index;
    private List<int> indexes;

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
        #endregion

        #region Attach Virtual Camera
        cinemachineController.SetVirtualCamera(_playerClone.transform);
        #endregion

        #region Set Spawn Manager
        _playerClone.GetComponent<PlayerController>().SetSpawnManager(this);
        #endregion

    }

    private void EnemySpawn()
    {

        indexes = new List<int>();
        int enemyCount = 0;

        for (int i = 0; i < _enemySpawnMarkers.Count; i++)
        {

            index = Random.Range(0, _enemySpawnMarkers.Count);

            if (!indexes.Contains(index))
            {
                indexes.Add(index);
                GameObject enemy = Instantiate(_enemyPrefab, _enemySpawnMarkers[indexes[i]].position, Quaternion.Euler(transform.up * Random.Range(0f, 360f)));
                enemy.transform.parent = _enemyParentTransform;
                enemy.name = "Peashooter" + (i + 1);
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                enemyController.SetSpawnManager(this);
                enemyController.SetPlayerTransform(_playerClone.transform);
                enemyCount++;
            }
            else
            {
                i--;
            }

            if (enemyCount == 1)
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

}
