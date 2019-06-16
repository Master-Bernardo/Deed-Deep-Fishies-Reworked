using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * one of the basic components which are used by an entity if needed
 * scans the world around it for other entities or other things, if need be
 * 
 * enery x seconds we update 
 */

[System.Serializable]
public class EC_Sensing : GameEntityComponent
{
    //Scanning World
    float nextScanWorldTime;
    public float scanWorldInterval;
    public float scanWorldRadius;

    //TODO collections for enemies and friendlies - to the enemy ai does not have t check this every frame
    public HashSet<GameEntity> visibleEntities = new HashSet<GameEntity>();
    public HashSet<GameEntity> friendlyFishies = new HashSet<GameEntity>();
    public HashSet<GameEntity> enemyFishies = new HashSet<GameEntity>();

    public HashSet<Consumable> visibleConsumables = new HashSet<Consumable>();
    public HashSet<Consumable> visibleEatableConsumables = new HashSet<Consumable>();

    public ConsumableType eatableConsumable;

    public bool showScanWorldRadius = false;
    public int entitiesLayer;
    public int consumablesLayer;

    Transform myTransform;



    public override void SetUpEntityComponent(GameEntity entity)
    {
        base.SetUpEntityComponent(entity);

        myTransform = entity.myTransform;
        nextScanWorldTime = Time.time + Random.Range(0, scanWorldInterval);       
    }

    //should not be lodded, is already lodded, optimise searching algorthm - hierarchical search using squads and distance check
    public override void UpdateEntityComponent(float deltaTime, float time)
    {
        if (time > nextScanWorldTime)
        {
            nextScanWorldTime += scanWorldInterval;

            ScanSurroundingUnits();
            ScanSurroundingConsumables();
        }
    }

    void ScanSurroundingUnits()
    {
        visibleEntities.Clear();
        friendlyFishies.Clear();
        enemyFishies.Clear();

        int layerMask = 1 << entitiesLayer;

        Collider2D[] visibleColliders = Physics2D.OverlapCircleAll(myTransform.position, scanWorldRadius, layerMask);

        for (int i = 0; i < visibleColliders.Length; i++)
        {
            GameEntity thisEntity = visibleColliders[i].GetComponent<GameEntity>();
            visibleEntities.Add(thisEntity);
            if(thisEntity.teamID != entity.teamID)
            {
                enemyFishies.Add(thisEntity);
            }
            else
            {
                friendlyFishies.Add(thisEntity);
            }
        }
    }

    void ScanSurroundingConsumables()
    {
        visibleConsumables.Clear();
        visibleEatableConsumables.Clear();

        int layerMask = 1 << consumablesLayer;

        Collider2D[] visibleColliders = Physics2D.OverlapCircleAll(myTransform.position, scanWorldRadius, layerMask);

        for (int i = 0; i < visibleColliders.Length; i++)
        {
            Consumable thisConsumable = visibleColliders[i].GetComponent<Consumable>();
            visibleConsumables.Add(thisConsumable);
            if(thisConsumable.type == eatableConsumable)
            {
                visibleEatableConsumables.Add(thisConsumable);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showScanWorldRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(myTransform.position, scanWorldRadius);
        }
    }
}
