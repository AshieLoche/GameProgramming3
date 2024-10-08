using UnityEngine;

[CreateAssetMenu(fileName = "New Week 6 Enemy",  menuName = "Week 6 Enemy")]
public class Week6_EnemySO : ScriptableObject
{
    public string enemyName;
    public int health;
    public int physicalAttack;
    public int armor;
    public int magicAttack;
    public int magicResist;
    public float speed;
}
