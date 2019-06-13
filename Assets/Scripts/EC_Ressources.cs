using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*
 * Holds track of all the ressources gathered by a creature. resourcs can be added or be consumed
 */
public class EC_Ressources : GameEntityComponent
{

    [System.Serializable]
    public class RessourceAmountPair
    {
        public ConsumableType ressource;
        public int amount;
    }

    public RessourceAmountPair[] ressources;

    public void AddRessource(ConsumableType type, int amount)
    {
        foreach (RessourceAmountPair pair in ressources)
        {
            if (pair.ressource == type) pair.amount += amount;
        }
    }

    public void RemoveRessource(ConsumableType type, int amount)
    {
        foreach (RessourceAmountPair pair in ressources)
        {
            if (pair.ressource == type) pair.amount -= amount;
        }
    }




}
