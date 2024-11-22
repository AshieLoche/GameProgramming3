using UnityEngine;
using UnityEngine.Events;

public class Week12_CollectionController : MonoBehaviour
{

    public static UnityEvent<int> OnAddCoin = new UnityEvent<int>();
    private int _coinsCollected = 0;

    private void Awake()
    {
        Week12_CoinCollect.OnCoinCollect.AddListener(AddCoin);
    }

    private void AddCoin()
    {
        OnAddCoin.Invoke(++_coinsCollected);
    }

}