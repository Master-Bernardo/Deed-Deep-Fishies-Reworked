using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [Tooltip("does the creature die if  this bodypart is destroyed?")]
    public bool vital;

    public GameEntity entity;

    //TODO refactor bodyparts inheritance - the whole vital shit and the interface implementation

}



