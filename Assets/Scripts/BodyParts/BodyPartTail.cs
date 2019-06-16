using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//later it can be used for movement
public class BodyPartTail : BodyPart, IDamageable<int>
{
    public HC_Health health;
    [Tooltip("this bodypart drops this when it gets destroyed")]
    public GameObject drop;

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        if (!health.alive)
        {
            if (vital) entity.Destroy();
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(5);
        }
    }

    private void OnDestroy()
    {
        GameObject drop1 = Instantiate(drop);
        drop1.transform.position = transform.position;
    }
}
