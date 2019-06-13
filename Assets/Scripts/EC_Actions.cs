using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * The actions class is the one corresponding with the controller, the controller only calls functions from the actions list. 
 * The action system carries theese functions out while consuming ressources
 */
public class EC_Actions : GameEntityComponent
{


    public override void SetUpEntityComponent(GameEntity entity)
    {
        base.SetUpEntityComponent(entity);
    }

    public void Consume(Consumable consumable)
    {

    }
}
