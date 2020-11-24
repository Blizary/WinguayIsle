using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMMooseWander : State
{
    public override void OnEnterState()
    {
        MooseAI ai = agent.GetComponent<MooseAI>();
        agent.GetComponent<Vehicle>().behaviours.Remove(agent.GetComponent<Flee>());
        agent.GetComponent<Cohesion>().weight = ai.coehVal.x;
        agent.GetComponent<Alignment>().weight = ai.aligVal.x;
        agent.GetComponent<Separation>().weight = ai.separVal.x;
        agent.GetComponent<ConstantSpeed>().speed = ai.speed.x;

    }

    public override void UpdateState()
    {
        if(agent.GetComponent<MooseAI>().wolfSpoted)
        {
            fsm.ActivateState<FSMMooseCauntion>();
        }
        
    }

    public override void OnExitState()
    {
        List<Vehicle> neighbours = agent.GetComponent<Separation>().neighbours;
        for (int i = 0; i < neighbours.Count; i++)
        {
            neighbours[i].GetComponent<MooseAI>().WolfHasBeenSpoted(agent.GetComponent<MooseAI>().target);
            Debug.Log("entered cauntion by wander");
        }
    }


}
