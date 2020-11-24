using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMMooseRun : State
{
    public override void OnEnterState()
    {
        MooseAI ai = agent.GetComponent<MooseAI>();

        agent.GetComponent<Vehicle>().behaviours.Add(agent.GetComponent<Flee>());
        agent.GetComponent<Cohesion>().weight = ai.coehVal.z;
        agent.GetComponent<Alignment>().weight = ai.aligVal.z;
        agent.GetComponent<Separation>().weight = ai.separVal.z;
        agent.GetComponent<ConstantSpeed>().speed = ai.speed.z;

    }

    public override void UpdateState()
    {
        if(!agent.GetComponent<MooseAI>().wolfClose)
        {
            fsm.ActivateState<FSMMooseCauntion>();
        }
    }
}
