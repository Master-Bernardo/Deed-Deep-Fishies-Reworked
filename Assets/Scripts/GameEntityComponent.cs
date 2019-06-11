using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * all the basic components derive from this class
 */


public class GameEntityComponent
{
    protected GameEntity entity; //every component has a reference to the entity using it - but does not have to use it by himself
    public bool enabled = true;

    public virtual void SetUpEntityComponent(GameEntity entity)
    {
        this.entity = entity;
    }

    public virtual void UpdateEntityComponent(float deltaTime, float time)
    {

    }

    public virtual void FixedUpdateEntityComponent(float deltaTime, float time)
    {

    }
 
}
