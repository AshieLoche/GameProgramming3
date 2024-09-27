using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week5GameManager : MonoBehaviour
{

    public static Week5GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] private DamageCalculator damageCalculator;

    public DamageCalculator DamageCalculator => damageCalculator;
}
