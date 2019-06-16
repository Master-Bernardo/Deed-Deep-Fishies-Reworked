using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*
 * The actions class is the one corresponding with the controller, the controller only calls functions from the actions list. 
 * The action system carries theese functions out while consuming ressources
 */
public class EC_Actions : GameEntityComponent
{
    EC_Ressources ressources;
    EC_Movement movement;

    [Tooltip("costPerSecond")]
    public RessourceAmountPair movementCost;
    public RessourceAmountPair passiveLivingCost;

    public override void SetUpEntityComponent(GameEntity entity)
    {
        base.SetUpEntityComponent(entity);
        ressources = (entity as Fishie).ressources;
        movement = (entity as Fishie).movement;
    }

    public override void UpdateEntityComponent(float deltaTime, float time)
    {
        base.UpdateEntityComponent(deltaTime, time);

        ressources.RemoveRessource(passiveLivingCost.type, passiveLivingCost.amount*Time.deltaTime);
    }


    public void Consume(Consumable consumable)
    {
        ressources.AddRessource(consumable.ressource.type, consumable.ressource.amount);
    }

    public void MoveToDestination(Vector2 destination)
    {
        //check if we have the necessary ressources
        if (ressources.IsThereEnoughOf(movementCost.type,movementCost.amount))
        {
            movement.MoveToDestination(destination);
            ressources.RemoveRessource(movementCost.type,movementCost.amount*Time.deltaTime);
        }

    }

    public void MoveToDirection(Vector2 direction)
    {
        if (ressources.IsThereEnoughOf(movementCost.type, movementCost.amount))
        {
            movement.MoveToDirection(direction);
            ressources.RemoveRessource(movementCost.type, movementCost.amount * Time.deltaTime);
        }
    }
    
}
