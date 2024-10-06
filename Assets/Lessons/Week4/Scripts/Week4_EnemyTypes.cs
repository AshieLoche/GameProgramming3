using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy Type")]
public class Week4_EnemyTypes : ScriptableObject
{

    [Header("Enemy Model")]
    public GameObject enemyPrefab;

    [Header("Enemy Stats")]
    public string enemyName;
    public int physicalDamage;
    public int magicalDamage;
    public int health;
    public int armor;
    public int magicResist;
    public float speed;

    [Header("Enemy Position")]
    public Vector3 position;

}
