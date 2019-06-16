using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RessourceType
{
    AquaVita,
    Treden //etc
}

public enum ConsumableType
{
    Plant,
    Meat
}


//somethin we can consume - a part of a plant or meat or some other ressource
public class Consumable : MonoBehaviour, IDamageable<int>
{
    public RessourceAmountPair ressource;
    public ConsumableType type;
    public void TakeDamage(int damage)
    {
        ressource.amount -= damage;
        if (ressource.amount <= 0) Destroy(gameObject);
    }
}
