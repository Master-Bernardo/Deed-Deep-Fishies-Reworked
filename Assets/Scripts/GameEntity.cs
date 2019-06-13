using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The container class which gathers all the basic components, which it wants to use is controlled by an entity AI which switches behaviours
 */

public class GameEntity : MonoBehaviour
{
    public int teamID;

    protected GameEntityComponent[] components;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public bool markAsDestroyed = false;





    protected virtual void Start()
    {
        GameEntityManager.Instance.AddGameEntity(this);

        myTransform = transform;

        for (int i = 0; i < components.Length; i++)
        {
           if(components[i].enabled) components[i].SetUpEntityComponent(this);
        }
    }

    public void FixedUpdateGameEntity(float deltaTime, float time)
    {
        for (int i = 0; i < components.Length; i++)
        {
            if(components[i].enabled) components[i].FixedUpdateEntityComponent(deltaTime, time);
        }
    }

    public void UpdateGameEntity(float deltaTime, float time)
    {
        for (int i = 0; i < components.Length; i++)
        {
            if (components[i].enabled) components[i].UpdateEntityComponent(deltaTime, time);
        }
    }

    public void Destroy()
    {
        markAsDestroyed = true;
    }

    

}
