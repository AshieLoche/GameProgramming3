using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] private List<Transform> _markers;
    [SerializeField] private GameObject _peashooter;
    [SerializeField]
    private Transform _enemies;
    private int index;
    private List<int> indexes;

    void Start()
    {

        indexes = new List<int>();
        int enemyCount = 0;

        for (int i = 0; i < _markers.Count; i++)
        {
            Instantiate(_peashooter, _markers[i]).transform.parent = _enemies;
            //index = Random.Range(0, _markers.Count);

            //if (!indexes.Contains(index))
            //{
            //    indexes.Add(index);
            //    Instantiate(_peashooter, _markers[indexes[i]]).transform.parent = _enemies;
            //    enemyCount++;
            //}
            //else
            //{
            //    i--;
            //}

            //if (enemyCount == 20)
            //{
            //    break;
            //}

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
