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


    public HashSet<GameEntity> visibleEntities = new HashSet<GameEntity>();


    public bool showScanWorldRadius = false;
    public int entitiesLayer;

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
        }
    }

    void ScanSurroundingUnits()
    {
        visibleEntities.Clear();

        int layerMask = 1 << entitiesLayer;

        Collider2D[] visibleColliders = Physics2D.OverlapCircleAll(myTransform.position, scanWorldRadius, layerMask);

        for (int i = 0; i < visibleColliders.Length; i++)
        {
            visibleEntities.Add(visibleColliders[i].GetComponent<GameEntity>());
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
