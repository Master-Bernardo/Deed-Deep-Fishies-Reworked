using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The container class which gathers all the basic components, which it wants to use is controlled by an entity AI which switches behaviours
 */

public class GameEntity : MonoBehaviour
{
    public int teamID;

    GameEntityComponent[] components;

    public EC_Sensing sensing;
    public EC_Movement movement;
    public EC_Health health;
    public PlayerController playerController;
    public EC_FishieAI fishieAI;
    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public bool markAsDestroyed = false;





    void Start()
    {
        GameEntityManager.Instance.AddGameEntity(this);

        //set the variables

        myTransform = transform;

        //setup all attached components
        components = new GameEntityComponent[] {sensing, movement, health, playerController, fishieAI}; 

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

    

}
