using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI enemyCounter;
    private int _enemyCount;

    void Update()
    {
        enemyCounter.text = "Enemy Count: " + _enemyCount.ToString();
    }

    public void SetEnemyCount(int enemyCount)
    {
        _enemyCount = enemyCount;
    }

}
