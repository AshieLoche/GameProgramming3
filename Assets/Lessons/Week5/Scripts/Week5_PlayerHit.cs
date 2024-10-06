using UnityEngine;

public class Week5_PlayerHit : Week5_CharacterStats
{

    private int _health = 50;
    private int _damage = 10;
    private int _maxHealth;

    protected override void SetStats()
    {
        _maxHealth = _health;
        base.SetStats();
    }

    private void Start()
    {
        SetStats();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Week5_GameManager.instance.damageCalculator.HitDamage(_health, _damage, this);
        }
    }

}
