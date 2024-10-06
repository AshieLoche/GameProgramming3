using UnityEngine;

public class Week5_GameManager : MonoBehaviour
{

    public static Week5_GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    [SerializeField] Week5_DamageCalculator _damageCalculator;
    public Week5_DamageCalculator damageCalculator => _damageCalculator;

}