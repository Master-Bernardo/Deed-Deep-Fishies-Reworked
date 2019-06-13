using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartSpike : BodyPart
{
    //TODO the dAMAGE AND THE FORCE APPLIED BY THE SPIKE ARE NOT PERFECTLY CALCULTAED

    public float baseDamage;
    [Tooltip("the force at which both collide gets multiplied by this value and added to the damage")]
    public float damageForceMultiplayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameEntity targetGameEntity = collision.gameObject.GetComponent<GameEntity>();
        BodyPart targetBodyPart = collision.gameObject.GetComponent<BodyPart>();
        /*
        if (targetGameEntity != null)
        {
            float multiplier = targetGameEntity.GetComponent<Rigidbody2D>().velocity.magnitude + entity.GetComponent<Rigidbody2D>().velocity.magnitude;

            (targetGameEntity as Fishie).health.TakeDamage((int)(baseDamage * multiplier * damageForceMultiplayer));

            //Debug.Log(multiplier);
            //Debug.Log((int)(baseDamage * multiplier * multiplier * damageForceMultiplayer));

            targetGameEntity.GetComponent<Rigidbody2D>().AddForce(transform.forward * multiplier*100);
        }
        else if (targetBodyPart != null)
        {
            float multiplier = targetBodyPart.GetComponent<Rigidbody2D>().velocity.magnitude + entity.GetComponent<Rigidbody2D>().velocity.magnitude;

            targetBodyPart.health.TakeDamage((int)(baseDamage * multiplier * damageForceMultiplayer));

            targetBodyPart.GetComponent<Rigidbody2D>().AddForce(transform.forward * multiplier*100);
        }*/
    }
}
