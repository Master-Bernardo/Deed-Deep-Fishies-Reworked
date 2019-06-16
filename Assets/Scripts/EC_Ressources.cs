using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RessourceAmountPair
{
    public RessourceType type;
    public float amount;
}

[System.Serializable]
public class RessourceSet
{
    public RessourceType type;
    public float amount;
    public bool vital;
    public float capacity;
}

/*
 * Holds track of all the ressources gathered by a creature. resourcs can be added or be consumed
 */
public class EC_Ressources : GameEntityComponent
{
    public RessourceSet[] ressources;

    public void AddRessource(RessourceType type, float amount)
    {
        foreach (RessourceSet item in ressources)
        {
            if (item.type == type)
            {
                item.amount += amount;
                if(item.amount>item.capacity) item.amount = item.capacity;
            }
        }
    }

    public void RemoveRessource(RessourceType type, float amount)
    {
        foreach (RessourceSet item in ressources)
        {
            if (item.type == type)
            {
                item.amount -= amount;
                if (item.amount <= 0)
                {
                    if (item.vital) entity.Destroy();
                }
            }
        }
    }

    public float GetRessourceAmount(RessourceType type)
    {
        foreach (RessourceSet item in ressources)
        {
            if (item.type == type)
            {
                return item.amount;
            }
        }

        return 0f;
    }

    public bool IsThereEnoughOf(RessourceType type, float amount)
    {
        foreach (RessourceSet item in ressources)
        {
            if (item.type == type) if (item.amount >= amount) return true;
        }

        return false;
    }




}
