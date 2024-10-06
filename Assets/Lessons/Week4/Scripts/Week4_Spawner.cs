using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week4_Spawner : MonoBehaviour
{

    [SerializeField] private List<Week4_EnemyTypes> _enemyTypes = new List<Week4_EnemyTypes>();

    private void Start()
    {
        foreach (Week4_EnemyTypes enemy in _enemyTypes)
        {
            GameObject newEnemy = Instantiate(enemy.enemyPrefab);
            newEnemy.AddComponent<Week4_EnemyStats>();
            Week4_EnemyStats stats = newEnemy.GetComponent<Week4_EnemyStats>();
            newEnemy.name = enemy.name;
            stats.enemyName = enemy.name;
            stats.physicalDamage = enemy.physicalDamage;
            stats.magicalDamage = enemy.magicalDamage;
            stats.health = enemy.health;
            stats.armor = enemy.armor;
            stats.magicResist = enemy.magicResist;
            stats.speed = enemy.speed;
            newEnemy.transform.position = enemy.position;
        }
    }

}
