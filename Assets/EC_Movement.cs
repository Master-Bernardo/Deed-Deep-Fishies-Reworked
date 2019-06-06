using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * one of the basic components which are used by an entity if needed
 * takes care of the navmesh agent implementation
 */

[System.Serializable]
public class EC_Movement : GameEntityComponent
{

    //movement
    public float movementAcceleration;
    public float maxMovementSpeed;

    [Tooltip("after changing rotationAcceleration or maxRotationSpeed or angularDrag, we need to recallibrate PID")]
    public float rotationAcceleration; //- is this needed?
    public float maxRotationSpeed;

    public Rigidbody2D rb;

    //is the vector pointing in which way we ant to go
    Vector2 desiredDirection = Vector2.zero;
    Vector2 myDirection;

    bool moveTo = false;

    //optimising transform call can be expensive on large scale
    Transform myTransform;


    //for PID Controller
    float pGain = 2f;
    float iGain = 50f;
    float dGain = 0.32f;
    float lastPError = 0;

    /* https://robotics.stackexchange.com/questions/167/what-are-good-strategies-for-tuning-pid-loops
     * 
     * 1. Set all gains to zero.
       2. Increase the P gain until the response to a disturbance is steady oscillation.
       3. Increase the D gain until the the oscillations go away (i.e. it's critically damped).
       4. Repeat steps 2 and 3 until increasing the D gain does not stop the oscillations.
       5. Set P and D to the last stable values.
       6.Increase the I gain until it brings you to the setpoint with the number of oscillations desired (normally zero but a quicker response can be had if you don't mind a couple oscillations of overshoot)
     */


    public override void SetUpEntityComponent(GameEntity entity)
    {
        base.SetUpEntityComponent(entity);
        myTransform = entity.myTransform;
       // nextMovementUpdateTime = Time.time + Random.Range(0, movementUpdateIntervall);
    }

    public override void FixedUpdateEntityComponent(float deltaTime, float time)
    {
        if (moveTo)
        {
            moveTo = false;
            if (rb.velocity.magnitude < maxMovementSpeed) rb.AddForce(myDirection * movementAcceleration);

            //PID Code
            float pError = Vector2.SignedAngle(myDirection, desiredDirection);
            float iError = pError * deltaTime;
            float dError = (pError - lastPError) / deltaTime;
            lastPError = pError;

            float torque = (pGain * pError + iGain * iError + dGain * dError) * rotationAcceleration;
            //we do nod necessary ned to multiply with rotationAcceleration - but it would be nice if this also makes a difference

            //cklamp - set a max rotation velocity
            if (torque > maxRotationSpeed) torque = maxRotationSpeed;
            else if (torque < -maxRotationSpeed) torque = -maxRotationSpeed;

            rb.AddTorque(torque);

        }


    }

    //the rotation is important for logic but it can be a bit jaggy if far away or not on screen - lod this script, only call it every x seconds
    /* public override void UpdateEntityComponent(float deltaTime, float time)
     {
         if (time > nextMovementUpdateTime)
         {
             nextMovementUpdateTime += movementUpdateIntervall;

         }
     }*/

    //for now simple moveTo without surface ship or flying
    public void MoveToDestination(Vector2 destination)
    {
        moveTo = true;
        Vector2 myPosition = myTransform.position;
        myDirection = myTransform.up;
        desiredDirection = destination - myPosition;
    }
}

