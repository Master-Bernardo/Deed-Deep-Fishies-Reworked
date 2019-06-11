using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntityManager : MonoBehaviour
{
    public static GameEntityManager Instance;

    HashSet<GameEntity> gameEntities = new HashSet<GameEntity>();

    //while in the update loop, some objects will be added here- they will be removed from the gameEntities list after the updateloop
    HashSet<GameEntity> entitiesToRemove = new HashSet<GameEntity>();



    void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void FixedUpdate()
    {
        float time = Time.time;
        float deltaTime = Time.deltaTime;

        foreach (GameEntity entity in gameEntities)
        {

            entity.FixedUpdateGameEntity(deltaTime, time);

        }
    }

    void Update()
    {
        float time = Time.time;
        float deltaTime = Time.deltaTime;

        foreach(GameEntity entity in gameEntities)
        {
            //Debug.Log("updating: " + entity.name +  " alive: " + entity.health.alive);

            if (!entity.markAsDestroyed)
            {
                entity.UpdateGameEntity(deltaTime, time);
            }
            else
            {
                MarkEntityToRemove(entity);
            }
        }

        //remove entities marked as remove
        foreach(GameEntity entity in entitiesToRemove)
        {
            gameEntities.Remove(entity);
            Destroy(entity.gameObject);
        }

        entitiesToRemove.Clear();
    }

    public void AddGameEntity(GameEntity entity)
    {
        gameEntities.Add(entity);
    }

    public void MarkEntityToRemove(GameEntity entity)
    {
        entitiesToRemove.Add(entity);
    }

}
