using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartMouth : BodyPart
{
    //the Trigger which this object cas triggers the bit, the actual damage radius of the bite is given by the radius value

    public int damage;

    public float biteRadius;

    public float biteInterval;
    [Tooltip("time from which the bite begann and when it ended")]
    public float biteTime;
    float nextBiteStartTime = 0;
    float nextBiteEndTime = Mathf.Infinity;

    public Animator biteAnimator;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time > nextBiteStartTime)
        {
            nextBiteStartTime = Time.time + biteInterval;
            nextBiteEndTime = Time.time + biteTime;

            biteAnimator.SetTrigger("Bite");
            
        }
       
    }

    private void Update()
    {
        if (Time.time > nextBiteEndTime)
        {
            nextBiteEndTime = Mathf.Infinity;

   
            //TODO sometimes it does not detect the enemy why?
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, biteRadius);

            GameEntity potentialGameEntity;
            GameEntity targetGameEntity = null;

            /*for (int i = 0; i < collisions.Length; i++)
            {
                potentialGameEntity = collisions[i].gameObject.GetComponent<GameEntity>();

                if (potentialGameEntity != null)
                {
                    if (potentialGameEntity != entity)
                    {
                        targetGameEntity = potentialGameEntity;
                    }
                }
            }

            if (targetGameEntity != null)
            {
                (targetGameEntity as Fishie).health.TakeDamage(damage);
                targetGameEntity.gameObject.GetComponent<Rigidbody2D>().AddForce((targetGameEntity.myTransform.position - entity.myTransform.position).normalized * 300);
            }*/

            /*else if (targetBodyPart != null)
            {
                if (targetBodyPart.hasHealth) targetBodyPart.health.TakeDamage(damage);
            }*/
        }
    }
}
