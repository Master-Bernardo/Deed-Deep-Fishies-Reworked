using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EC_FishieAI : GameEntityComponent
{
    EC_Sensing sensing;
    EC_Actions actions;
    EC_Ressources ressources;
    Transform myTransform;

    public bool hunter;
    public float reproductionTreshold;


    Vector2 currentWanderDestination;

    public override void SetUpEntityComponent(GameEntity entity)
    {
        base.SetUpEntityComponent(entity);
        myTransform = entity.transform;
        sensing = (entity as Fishie).sensing;
        actions = (entity as Fishie).actions;
        ressources = (entity as Fishie).ressources;

        currentWanderDestination = Vector2.zero;

    }

    public override void UpdateEntityComponent(float deltaTime, float time)
    {
        //if i have enough energy - dublicate, the copy and i get half of the current energy
        if (ressources.GetRessourceAmount(RessourceType.AquaVita) > reproductionTreshold)
        {
            GameObject copy = Instantiate(gameObject);
            copy.transform.position = transform.position + new Vector3(0.1f, 0f, 0f);
            ressources.RemoveRessource(RessourceType.AquaVita, reproductionTreshold / 2);
            copy.GetComponent<EC_Ressources>().RemoveRessource(RessourceType.AquaVita, reproductionTreshold / 2);
        }

        //TODO hunter not working as intended
        if (hunter)
        {

            //1. check if there is food nearby
            if (sensing.visibleEatableConsumables.Count > 0)
            {
                PursueNearestFood();
            }
            else if (sensing.enemyFishies.Count > 0)
            {
                PursueNearestEnemy();
            }
            else
            {
                Wander();
            }
        }
        //if not hunter
        else
        {
            //1. check if there is food nearby
            if (sensing.enemyFishies.Count>0)
            {
                RunFormNearestEnemy();
            }
            else if (sensing.visibleEatableConsumables.Count > 0)
            {
                PursueNearestFood();
            }
            else
            {
                Wander();
            }
        }

    }

    void PursueNearestEnemy()
    {
        float nearestDistance = Mathf.Infinity;
        GameEntity nearestTarget = null;

        foreach (GameEntity thisEntity in sensing.enemyFishies)
        {
            if (thisEntity != null)
            {
                if (thisEntity.teamID != entity.teamID)
                {
                    float currentDistance = (thisEntity.myTransform.position - myTransform.position).sqrMagnitude;

                    if (currentDistance < nearestDistance)
                    {
                        nearestDistance = currentDistance;
                        nearestTarget = thisEntity;
                    }
                }
            }
        }

        if (nearestTarget != null) actions.MoveToDestination(nearestTarget.transform.position);
    }

    void RunFormNearestEnemy()
    {
        float nearestDistance = Mathf.Infinity;
        GameEntity nearestTarget = null;

        foreach (GameEntity thisEntity in sensing.enemyFishies)
        {
            if (thisEntity != null)
            {
                if (thisEntity.teamID != entity.teamID)
                {
                    float currentDistance = (thisEntity.myTransform.position - myTransform.position).sqrMagnitude;

                    if (currentDistance < nearestDistance)
                    {
                        nearestDistance = currentDistance;
                        nearestTarget = thisEntity;
                    }
                }
            }
        }

        if(nearestTarget!=null)actions.MoveToDirection(transform.position-nearestTarget.transform.position);
    }

    void PursueNearestFood()
    {
        float nearestDistance = Mathf.Infinity;
        Consumable nearestConsumable = null;

        foreach (Consumable consumable in sensing.visibleEatableConsumables)
        {
            if (consumable != null)
            {
                float currentDistace = (consumable.transform.position - myTransform.position).sqrMagnitude;

                if (currentDistace < nearestDistance)
                {
                    nearestConsumable = consumable;
                }
            }
                 

        }

        if(nearestConsumable!=null) actions.MoveToDestination(nearestConsumable.transform.position);
    }


    void Wander()
    {
        if (Time.frameCount % 30 == 0)
        {
            Vector3 random = Random.insideUnitSphere;
            random.z = 0;
            currentWanderDestination = transform.position + transform.up * 5 + random * 5;
        }

        actions.MoveToDestination(currentWanderDestination);
    }
}
