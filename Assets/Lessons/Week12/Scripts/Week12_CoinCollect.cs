using UnityEngine;
using UnityEngine.Events;

public class Week12_CoinCollect : MonoBehaviour
{

    public static UnityEvent OnCoinCollect = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCoinCollect.Invoke();
            Destroy(gameObject);
        }
    }

}