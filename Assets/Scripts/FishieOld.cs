using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishieOld : MonoBehaviour {

    public Rigidbody2D rb;
    //public Transform target = null; //target to which we swimm

    //movement
    public float movementForce;
    public float maxSpeed;

    public float rotationSpeed;
    public float maxRotation;

    //hp and strength (eating makes strength and hp and size bigger
    public float size;
    public float strength;

    public int maxHp;
    private float currentHP;

    //attack rate
    public float attackIntervall;
    private float nextAttackTime;

    //for steering
    public bool AIEnabled = true;
    private Vector2 desiredVelocity;

    private Transform seekTarget;
    private List<Transform> fleeTargets;

    //for seek and flee
    public float seekRadius = 15f;
    public float fleeRadius = 10f;
   

    //for wander
    float nextWanderPointTime;
    public float wanderPointIntervall = 1f;
    private Vector3 currentWanderPoint;

    public enum FishieState
    {
        Seek,
        Flee,
        Wander
    }

    public FishieState state;

    public enum FishieType  // für irgendwann später vielleicht? 
    {
        Fleischesser,
        Pflanzenfresser,
        Allesfresser
    }

    private void Awake()
    {
        nextWanderPointTime = Time.time;
        fleeTargets = new List<Transform>();
        currentHP = maxHp;
        nextAttackTime = Time.time + nextAttackTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (AIEnabled)
        {
            UpdateSeekAndFleeTargets();
        }
    }
    private void FixedUpdate()
    {
        if (AIEnabled)
        {
            //desired velocity Vector rausfinden:

            if (seekTarget != null)
            {
                desiredVelocity = Seek(seekTarget.position) * 1f;
            }else
            {
                desiredVelocity = Wander();
            }
            if (fleeTargets.Count > 0)
            {
                foreach (Transform fleeTarget in fleeTargets)
                {
                    //Debug.Log("Flee target: " + fleeTarget);
                    //TODO weighting based on distance
                    float distance = Vector2.Distance(transform.position, fleeTarget.position); //low1 = fleeRadius, low 2 0  //low 2 =0.05, high2 = 2
                    float low1 = fleeRadius;
                    float low2 = 0.05f;
                    float high1 = 0f;
                    float high2 = 1f;

                    float weight = low2 + (distance - low1) * (high2 - low2) / (high1 - low1);

                    float strengthDifference = 0f;

                    if (fleeTarget.gameObject.GetComponent<FishieOld>() != null) strengthDifference = fleeTarget.GetComponent<FishieOld>().strength - strength;

                    weight *= 1+strengthDifference;

                    //if(fleeTarget.gameObject.tag=="obstacle")


                    //do this with an animation curve?
                    //desiredVelocity += Flee(fleeTarget.position) * 0.7f * (1 / fleeTargets.Count);
                    desiredVelocity += Flee(fleeTarget.position) * weight;
                }

                //Debug.Log("flee");

                /*if (seekTarget != null)
                {
                    foreach (Transform fleeTarget in fleeTargets)
                    {
                        Debug.Log("Flee targetWhileSeek: " + fleeTarget);
                        //TODO weighting based on distance
                        desiredVelocity += Flee(fleeTarget.position) * 0.4f * (1 / fleeTargets.Count);
                    }
                }
                else
                {
                    foreach (Transform fleeTarget in fleeTargets)
                    {
                        Debug.Log("Flee target: " + fleeTarget);
                        //TODO weighting based on distance
                        desiredVelocity += Flee(fleeTarget.position) * 0.7f * (1 / fleeTargets.Count);
                    }
                }*/
            }

            //Debug.Log("desiredVelocity: " + desiredVelocity);
            if (rb.velocity.magnitude < maxSpeed) rb.AddForce(transform.up * movementForce);

            transform.up = Vector3.Slerp(transform.up, desiredVelocity, rotationSpeed * Time.deltaTime);
        }
    }

    //set the targets which we seek and from which we flee ( we flee from targets in our near for collisuin avoidance
    private void UpdateSeekAndFleeTargets()
    {
        fleeTargets.Clear();
        seekTarget = null;

        //Get nearby flee objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, fleeRadius);  

        foreach (Collider2D nearbyObject in colliders)
        {
            //if (nearbyObject.gameObject.tag != "food" && nearbyObject.transform.root != transform && nearbyObject.gameObject.tag != "Player" && nearbyObject.gameObject.tag != "mouthZone") //root notwendig damit wir uns selber ignorieren
            if (nearbyObject.gameObject.tag == "fishie" && nearbyObject.transform.root != transform || nearbyObject.gameObject.tag == "Player" && nearbyObject.transform.root != transform) //root notwendig damit wir uns selber ignorieren
            {
                if (nearbyObject.gameObject.GetComponent<FishieOld>() != null)
                {
                    if (nearbyObject.gameObject.GetComponent<FishieOld>().strength > strength)
                    {
                        fleeTargets.Add(nearbyObject.transform);
                    }else if (Vector2.Distance(transform.position, nearbyObject.transform.position) < fleeRadius / 4 && nearbyObject.transform != seekTarget) // && wir verfolgen ihn nicht
                    {
                        fleeTargets.Add(nearbyObject.transform);
                    }
                }
            }
            else if (nearbyObject.gameObject.tag == "obstacle")
            {
                fleeTargets.Add(nearbyObject.transform);
            }
        }

        //search for other seek objects
        Collider2D[] seekColliders = Physics2D.OverlapCircleAll(transform.position, seekRadius);

        foreach (Collider2D nearbyObject in seekColliders)
        {
            //if (nearbyObject.gameObject.tag == "Player")
            //{
            if (nearbyObject.gameObject.GetComponent<FishieOld>() != null)
            {
                if (nearbyObject.gameObject.GetComponent<FishieOld>().strength < strength)
                {
                    seekTarget = nearbyObject.transform;
                }
            }
            //}
        }

    }

    #region steering
    private Vector2 SeekAndFlee(Vector2 _target, bool invert) //invert is true for flee
    {
        Vector2 _desiredVelocity;
        _desiredVelocity = _target - new Vector2(transform.position.x, transform.position.y);
        _desiredVelocity = _desiredVelocity.normalized * movementForce;

        if (invert)
        {
            _desiredVelocity = -_desiredVelocity;
            //_target = new Vector2(transform.position.x,transform.position.y) + _desiredVelocity * 2;
        }
        
        return _desiredVelocity;
    }

    public Vector2 Flee(Vector2 _target)
    {
        return SeekAndFlee(_target, true);
    }

    public Vector2 Seek(Vector2 _target)
    {
        return SeekAndFlee(_target, false);
    }

    public Vector2 Wander()
    {
        if (Time.time > nextWanderPointTime)
        {
            currentWanderPoint = Random.insideUnitSphere;
            currentWanderPoint = new Vector3(currentWanderPoint.x*2, currentWanderPoint.y*2 + 5, 0f); //transform to world space
            //Debug.Log("local: " + currentWanderPoint);
            currentWanderPoint = transform.TransformPoint(currentWanderPoint);
            //Debug.Log("world: " + currentWanderPoint);
            nextWanderPointTime = Time.time + Random.Range(wanderPointIntervall/2,wanderPointIntervall);

        }
        
        return Seek(currentWanderPoint);
    }
    #endregion

    public void PlayerMoveTowards(Vector2 _target)
    {
        desiredVelocity = Seek(_target);

        if (rb.velocity.magnitude < maxSpeed) rb.AddForce(transform.up * movementForce);

        transform.up = Vector3.Slerp(transform.up, desiredVelocity, rotationSpeed * Time.deltaTime);
    }

    public void EatFood(GameObject target) //temporary
    { 
        Destroy(target);
    }

    public void Attack(FishieOld targetFishie) //wird ausgeführt wenn etwas in dr Mouth zine ist - für food und auch für feinde(falls fleischesser)
    {
        if (Time.time > nextAttackTime) {
            targetFishie.GetDamage(strength);
            nextAttackTime = Time.time + attackIntervall;
        }
    }

    public void GetDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
