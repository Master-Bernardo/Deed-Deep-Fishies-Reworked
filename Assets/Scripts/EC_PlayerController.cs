using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EC_PlayerController : GameEntityComponent {

    public Camera mainCam;
    EC_Actions actions;

    //for movement
    Vector2 clickedPoint;

    public override void SetUpEntityComponent(GameEntity entity)
    {
        base.SetUpEntityComponent(entity);
        actions = (entity as Fishie).actions;
    }

    public override void UpdateEntityComponent(float deltaTime, float time)
    {
        
        //lmb
        if (Input.GetMouseButton(0))
        {
            //simple movement code
            clickedPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
            actions.MoveToDestination(clickedPoint);
        } 
    }
}
