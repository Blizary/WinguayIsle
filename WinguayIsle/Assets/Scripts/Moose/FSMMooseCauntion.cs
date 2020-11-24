using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMMooseCauntion : State
{
    public override void OnEnterState()
    {
        MooseAI ai = agent.GetComponent<MooseAI>();

        agent.GetComponent<Cohesion>().weight = ai.coehVal.y;
        agent.GetComponent<Alignment>().weight = ai.aligVal.y;
        agent.GetComponent<Separation>().weight = ai.separVal.y;
        agent.GetComponent<ConstantSpeed>().speed = ai.speed.y;

    }

    public override void UpdateState()
    {
        if (agent.GetComponent<MooseAI>().wolfClose)
        {
            fsm.ActivateState<FSMMooseRun>();
        }
        else if (!agent.GetComponent<MooseAI>().wolfClose)
        {
            List<Vehicle> neighbours = agent.GetComponent<Separation>().neighbours;
            bool isWolfClose = false;
            for (int i = 0; i < neighbours.Count; i++)
            {
                if (!isWolfClose)
                {
                    if (neighbours[i].GetComponent<MooseAI>().wolfSpoted)
                    {
                        isWolfClose = true;
                    }
                }
                
            }

            if(!isWolfClose)
            {
                agent.GetComponent<MooseAI>().wolfSpoted = false;
                fsm.ActivateState<FSMMooseWander>();
            }
        }

    }

}
