using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishie : GameEntity
{
    public EC_Sensing sensing;
    public EC_Movement movement;
    public EC_Actions actions;
    public EC_Ressources ressources;
    public EC_PlayerController playerController;
    public EC_FishieAI fishieAI;

    [Tooltip("the main body which holds everything together")]
    public BodyPart mainBody;

    [Tooltip("this bodypart drops this when it gets destroyed")]
    public GameObject drop;

    bool alive;


    protected override void Start()
    {
        sensing = GetComponent<EC_Sensing>();
        movement = GetComponent<EC_Movement>();
        playerController = GetComponent<EC_PlayerController>();
        fishieAI = GetComponent<EC_FishieAI>();
        ressources = GetComponent<EC_Ressources>();
        actions = GetComponent<EC_Actions>();

        //setup all attached components
        if(playerController!=null) components = new GameEntityComponent[] { sensing, movement, actions, ressources, playerController };
        else if(fishieAI!=null) components = new GameEntityComponent[] { sensing, movement, actions, ressources, fishieAI };

        base.Start();

    }
}
