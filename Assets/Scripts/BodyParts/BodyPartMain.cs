using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartMain : BodyPart, IDamageable<int>
{
    public HC_Health health;

    [Tooltip("this bodypart drops this when it gets destroyed")]
    public GameObject drop;


    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        if(!health.alive)
        {
            if (vital) entity.Destroy();
        }
    }

    private void OnDestroy()
    {
        GameObject drop1 = Instantiate(drop);
        drop1.transform.position = transform.position + new Vector3(0.1f,0f,0f);
        GameObject drop2 = Instantiate(drop);
        drop2.transform.position = transform.position;
    }
}
