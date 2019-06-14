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

    public void AddRessource(RessourceAmountPair ressource)
    {
        foreach (RessourceSet item in ressources)
        {
            if (item.type == ressource.type) item.amount += ressource.amount;
        }
    }

    public void RemoveRessource(RessourceAmountPair ressource)
    {
        foreach (RessourceSet item in ressources)
        {
            if (item.type == ressource.type)
            {
                item.amount -= ressource.amount;
                if (item.amount <= 0)
                {
                    if (item.vital) entity.Destroy();
                }
            }
        }
    }

    public bool IsThereEnoughOf(RessourceAmountPair ressource)
    {
        foreach (RessourceSet item in ressources)
        {
            if (item.type == ressource.type) if (item.amount >= ressource.amount) return true;
        }

        return false;
    }




}
