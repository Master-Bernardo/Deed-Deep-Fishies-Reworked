using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EC_PlayerController : GameEntityComponent {

    public Camera mainCam;
    EC_Movement movement;

    //for movement
    Vector2 clickedPoint;

    public override void SetUpEntityComponent(GameEntity entity)
    {
        base.SetUpEntityComponent(entity);
        movement = (entity as Fishie).movement;
    }

    public override void UpdateEntityComponent(float deltaTime, float time)
    {
        
        //lmb
        if (Input.GetMouseButton(0))
        {
            //simple movement code
            clickedPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
            movement.MoveToDestination(clickedPoint);
        } 
    }
}
