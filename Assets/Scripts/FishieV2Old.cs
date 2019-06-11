using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishieV2Old : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform target = null; //target to which we swimm

    //movement
    public float movementForce;
    public float maxSpeed;

    public float rotationSpeed;
    public float maxRotation;
    public float toleranz = 5;
    public float abbremswinkel = 25;

    public bool AIEnabled = true;

    private Vector2 desiredVelocity;

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
    private void Awake()
    {
        nextWanderPointTime = Time.time;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (target != null && AIEnabled)// && seek ==true)
        {
            if (state == FishieState.Seek) Seek(target.position);
            else if (state == FishieState.Flee) Flee(target.position);
            else if (state == FishieState.Wander) Wander();
        }
    }

    #region steering
    public void SeekAndFlee(Vector2 _target, bool invert) //invert is true for flee
    {

        desiredVelocity = _target - new Vector2(transform.position.x, transform.position.y);
        desiredVelocity = desiredVelocity.normalized * movementForce;

        if (invert)
        {
            desiredVelocity = -desiredVelocity;
            _target = new Vector2(transform.position.x, transform.position.y) + desiredVelocity * 2;
        }

        if (rb.velocity.magnitude < maxSpeed) rb.AddForce(transform.up * movementForce);

        //rotation code
        //new way
        transform.up = Vector3.Slerp(transform.up, desiredVelocity, rotationSpeed * Time.deltaTime);


        /* old way
        //TODO Question: How much steering to add?!
        
        //calculate the difference
         float angleDifference = Vector2.Angle(desiredVelocity, transform.up);
         float steering = 0;

        if (transform.InverseTransformPoint(_target).x > 0.2) // wen der punkt weiter rechts ist
        {
            if (angleDifference > toleranz)
            {
                if (angleDifference < abbremswinkel)
                {
                    steering = -(angleDifference * rotationSpeed / abbremswinkel);
                    steering -= rb.angularVelocity;
                    if (steering > 0) steering = 0;
                }
                else
                {
                    steering = -1 * rotationSpeed;
                    //steering+=rb.angularVelocity;

                }
            }

        }
        else if (transform.InverseTransformPoint(_target).x < 0.2)
        {
            if (angleDifference > toleranz)
            {
                if (angleDifference < abbremswinkel)
                {
                    steering = angleDifference * rotationSpeed / abbremswinkel;
                    steering -= rb.angularVelocity;
                    if (steering < 0) steering = 0;
                }
                else
                {
                    steering = 1 * rotationSpeed;
                    //steering -= rb.angularVelocity;
                    //if (steering < 0) steering = 0;
                }
            }

        }

        //clamp 
        if (steering < -maxRotation) steering = -maxRotation;
        else if (steering > maxRotation) steering = maxRotation;
        rb.AddTorque(steering);
        */



    }

    public void Flee(Vector2 _target)
    {
        SeekAndFlee(_target, true);
    }

    public void Seek(Vector2 _target)
    {
        SeekAndFlee(_target, false);
    }

    public void Wander()
    {
        if (Time.time > nextWanderPointTime)
        {
            currentWanderPoint = Random.insideUnitSphere;
            currentWanderPoint = new Vector3(currentWanderPoint.x * 2, currentWanderPoint.y * 2 + 5, 0f); //transform to world space
            Debug.Log("local: " + currentWanderPoint);
            currentWanderPoint = transform.TransformPoint(currentWanderPoint);
            Debug.Log("world: " + currentWanderPoint);
            nextWanderPointTime = Time.time + Random.Range(wanderPointIntervall / 2, wanderPointIntervall);


        }
        Seek(currentWanderPoint);
    }
    #endregion

    public void EatFood(GameObject target)
    {
        Destroy(target);
    }

}
