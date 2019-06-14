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
            /*bool bite = false;

            BodyPart potentialBodyPart = collision.gameObject.GetComponent<BodyPart>();
            if (potentialBodyPart != null)
            {
                if (potentialBodyPart.entity != entity)
                {
                    bite = true;
                }
            }else
            {
                Consumable potentialConsumable = collision.gameOvjec
            }*/
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
            BodyPart potentialBodyPart;
            Consumable potentialConsumable;

            for (int i = 0; i < collisions.Length; i++)
            {
                potentialBodyPart = collisions[i].gameObject.GetComponent<BodyPart>();

                if (potentialBodyPart != null)
                {
                    if(potentialBodyPart.entity != entity)
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
                        (entity as Fishie).actions.Consume(potentialConsumable);
                    }
                }

                

                
            }
        }
    }
}
