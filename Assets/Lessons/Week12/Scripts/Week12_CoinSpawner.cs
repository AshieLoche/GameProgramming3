using UnityEngine;

public class Week12_CoinSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _coinParent;
    [SerializeField] private int _coinAmount;
    private Transform _coinClone;

    private void Start()
    {
        SpawnCoin();
    }

    private void SpawnCoin(int coinCount = 1)
    {
        _coinClone = Instantiate(_coinPrefab).transform;
        _coinClone.position = new Vector3(Random.Range(-45f, 45f), _coinClone.position.y, Random.Range(-45f, 45f));
        _coinClone.name = $"Coin {coinCount}";
        _coinClone.parent = _coinParent;

        if (_coinAmount > coinCount)
        {
            SpawnCoin(++coinCount);
        }
    }

}