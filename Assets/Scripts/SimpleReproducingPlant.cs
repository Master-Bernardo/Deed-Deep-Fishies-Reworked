using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * C_ stands for consumable
 * this simple plant is eatable and dublicates itself every x seconds
 */
public class SimpleReproducingPlant : MonoBehaviour
{
    public GameObject plantConsumablePrefab;
    public float reproductionIntervall;
    float nextReproductionTime;


    // Start is called before the first frame update
    void Start()
    {
        nextReproductionTime = Time.time + Random.Range(reproductionIntervall/2,reproductionIntervall);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextReproductionTime)
        {
            nextReproductionTime += Random.Range(reproductionIntervall/2, reproductionIntervall);
            GameObject dublicate = Instantiate(plantConsumablePrefab);
            dublicate.transform.position = transform.position + new Vector3(0.1f,0f,0f);
        }
    }
}
