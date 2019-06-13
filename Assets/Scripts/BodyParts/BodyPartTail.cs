using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//later it can be used for movement
public class BodyPartTail : BodyPart, IDamageable<int>
{
    public HC_Health health;

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        if (!health.alive)
        {
            if (vital) entity.Destroy();
            else Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(5);
        }
    }
}
