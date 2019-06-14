﻿using System.Collections;
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

        ressources.RemoveRessource(passiveLivingCost);
    }

    public void Consume(Consumable consumable)
    {
        ressources.AddRessource(consumable.ressource);
    }

    public void MoveToDestination(Vector2 destination)
    {
        //check if we have the necessary ressources
        if (ressources.IsThereEnoughOf(movementCost))
        {
            movement.MoveToDestination(destination);
            ressources.RemoveRessource(movementCost);
        }

    }

    public void MoveToDirection(Vector2 direction)
    {
        if (ressources.IsThereEnoughOf(movementCost))
        {
            movement.MoveToDirection(direction);
            ressources.RemoveRessource(movementCost);
        }
    }
    
}
