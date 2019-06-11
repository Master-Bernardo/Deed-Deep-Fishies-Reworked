using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EC_FishieAI : GameEntityComponent
{
    EC_Sensing sensing;
    EC_Movement movement;
    Transform myTransform;

    public override void SetUpEntityComponent(GameEntity entity)
    {
        base.SetUpEntityComponent(entity);
        myTransform = entity.transform;
        sensing = entity.sensing;
        movement = entity.movement;
    }

    public override void UpdateEntityComponent(float deltaTime, float time)
    {
        float nearestDistance = Mathf.Infinity;
        GameEntity nearestTarget = null;

        foreach (GameEntity thisEntity in sensing.visibleEntities)
        {
            if(thisEntity.teamID != entity.teamID)
            {
                float currentDistance = (thisEntity.myTransform.position - myTransform.position).sqrMagnitude;

                if (currentDistance < nearestDistance)
                {
                    nearestDistance = currentDistance;
                    nearestTarget = thisEntity;
                }
            }     
        }

        if (nearestTarget != null)
        {
            if (entity.teamID == 0)
            {
                movement.MoveToDirection(nearestTarget.myTransform.position - myTransform.position);
            }
            else if (entity.teamID == 1)
            {
                movement.MoveToDirection(myTransform.position - nearestTarget.myTransform.position);
            }
        }


    }
}
