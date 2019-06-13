using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    AquaVita,
    Treden //etc
}


//somethin we can consume - a part of a plant or meat or some other ressource
public class Consumable : MonoBehaviour
{
    public ConsumableType type;
    public int amount;
}
