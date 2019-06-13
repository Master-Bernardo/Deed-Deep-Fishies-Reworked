using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartMain : BodyPart, IDamageable<int>
{
    public HC_Health health;

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        if(!health.alive)
        {
            if (vital) entity.Destroy();
        }
    }
}
