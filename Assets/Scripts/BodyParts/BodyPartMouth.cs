using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartMouth : BodyPart
{
    //the Trigger which this object cas triggers the bit, the actual damage radius of the bite is given by the radius value

    public int damage;
    public ConsumableType typeOfEatibleFood;
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
            bool bite = false;

            BodyPart potentialBodyPart = collision.gameObject.GetComponent<BodyPart>();
            if (potentialBodyPart != null)
            {
                if (potentialBodyPart.entity.teamID != entity.teamID)
                {
                    bite = true;
                }
            }else
            {
                //check if we can eat this consumable
                Consumable potentialConsumable = collision.gameObject.GetComponent<Consumable>();
                if (potentialConsumable != null)
                {
                    if(potentialConsumable.type == typeOfEatibleFood)
                    {
                        bite = true;
                    }
                }
            }

            if (bite)
            {
                nextBiteStartTime = Time.time + biteInterval;
                nextBiteEndTime = Time.time + biteTime;

                biteAnimator.SetTrigger("Bite");
            }   
            
        }
       
    }

    private void Update()
    {
        if (Time.time > nextBiteEndTime)
        {
            nextBiteEndTime = Mathf.Infinity;

   
            //TODO sometimes it does not detect the enemy why?
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, biteRadius);
            BodyPart potentialBodyPart;
            Consumable potentialConsumable;

            for (int i = 0; i < collisions.Length; i++)
            {
                potentialBodyPart = collisions[i].gameObject.GetComponent<BodyPart>();

                if (potentialBodyPart != null)
                {
                    if(potentialBodyPart.entity.teamID != entity.teamID) //friendly fire check
                    {
                        if (potentialBodyPart is IDamageable<int>)
                        {
                            (potentialBodyPart as IDamageable<int>).TakeDamage(damage);
                            return;
                        }
                    }
                    
                }else
                {
                    potentialConsumable = collisions[i].gameObject.GetComponent<Consumable>();

                    if (potentialConsumable!= null)
                    {
                        (potentialConsumable as IDamageable<int>).TakeDamage(damage);
                        if(potentialConsumable.type == typeOfEatibleFood)    (entity as Fishie).actions.Consume(potentialConsumable);
                    }
                }

                

                
            }
        }
    }
}
